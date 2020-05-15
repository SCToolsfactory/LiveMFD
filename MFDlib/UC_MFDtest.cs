using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFDlib.Drawing;

using MFDlib.MfdPanel;

namespace MFDlib
{
  /// <summary>
  /// Implements one instance of a particular MFD
  /// 
  /// Uses Infrastructure from the DLL to do so
  /// Implementing Code should be placed in MfdImplementation.(UC_)MFDname.cs
  /// Restrict code here to the minimum
  /// </summary>
  public partial class UC_MFDtest : UserControl, IUC_MFD, IMfdSupport
  {
    /// <summary>
    /// MFD Panels backing object
    /// </summary>
    private MfdImplementation.MFDtest m_mfd = new MfdImplementation.MFDtest( );


    /// <summary>
    /// Exposes a command callback derived from COMMAND "Ext3" 
    /// </summary>
    public event EventHandler<UC_MFD_CmdEventArgs> CommandEvent;
    // The command callback implementation - used by MfdSupport
    void IMfdSupport.Command( MfdInstance mfd, string commandArgument, bool pressed )
    {
      // just send it to the Owner Form - no local processing in the UC
      CommandEvent?.Invoke( this, new UC_MFD_CmdEventArgs( mfd, commandArgument, pressed ) );
    }

    // Implements the IF to handle data from SC API Server
    public void SCDataUpdate( int data )
    {
      throw new NotImplementedException( ); // well we wait for it..
    }

    /// <summary>
    /// Initialize the MFD
    ///  aquires the MFD device, reads the config and establishes a SCJoyServer client
    /// </summary>
    /// <param name="mfd">The MFD instance to handle</param>
    /// <param name="configString">A config string to assign buttons, labels</param>
    /// <param name="ip">The IP address of the SCJoyServer</param>
    /// <param name="port">The first Joystick port of the Server</param>
    /// <returns>True if successfull</returns>
    public bool InitMFD( MfdInstance mfd, string configString, string ip = "", uint port = 0 )
    {
      return m_mfd.InitMFD( this, mfd, configString, ip, port );
    }

    /// <summary>
    /// Cleanup and release Devices
    /// </summary>
    public void Shutdown()
    {
      m_mfd.MFD.Shutdown( );
    }


    // UC handling

    /// <summary>
    /// Out of thread refresh
    /// </summary>
    private delegate void RefreshMe();
    private RefreshMe myRefreshMe;
    private void RefreshMe_Method()
    {
      this.Refresh( );
    }
    /// <summary>
    /// Handles the external refresh 
    /// </summary>
    public void ExternalRefresh()
    {
      if ( this.InvokeRequired ) {
        this.Invoke( myRefreshMe );
      }
      else {
        RefreshMe_Method( );
      }
    }

    public UC_MFDtest()
    {
      InitializeComponent( );

      myRefreshMe = new RefreshMe( RefreshMe_Method );
    }

    private void UC_MFDtest_Load( object sender, EventArgs e )
    {

    }

    // UC paint event handler
    private void UC_MFDtest_Paint( object sender, PaintEventArgs e )
    {
      if ( this.Visible ) {
        m_mfd.Paint( e.Graphics ); // just call the MfdSupport Code
      }
    }

    private void UC_MFDtest_VisibleChanged( object sender, EventArgs e )
    {
      m_mfd.MFD.ActivateDxInput( this.Visible ); // activate if visible
      if ( !this.Visible ) {
        m_mfd.MFD.DeactivateAllDisplayItems( );
      }
    }

    /// <summary>
    /// Button is pressed
    ///  A caller can use this to trigger a Button Press 
    /// </summary>
    /// <param name="button">Button No 1..28</param>
    public void ButtonPress( int button )
    {
      if ( button < 1 ) return;
      if ( button > 28 ) return;

      m_mfd.MFD.ButtonPress( button );
    }

    /// <summary>
    /// Button is released
    ///  A caller can use this to trigger a Button Release
    /// </summary>
    /// <param name="button">Button No 1..28</param>
    public void ButtonRelease( int button )
    {
      if ( button < 1 ) return;
      if ( button > 28 ) return;

      m_mfd.MFD.ButtonRelease( button );
    }

  }
}
