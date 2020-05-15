using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MFDlib;
using MFDlib.OwnerSupport;

using MFDstartLand;
using MFDflight;

namespace TEST_MFD
{
  public partial class Form1 : Form
  {
    // A Support Queue allowing to switch through the loaded MFDs
    private MfdCycler m_leftMfds = new MfdCycler( );
    private MfdCycler m_rightMfds = new MfdCycler( );

    #region Form Handling

    /// <summary>
    /// Checks if a rectangle is visible on any screen
    /// </summary>
    /// <param name="formRect"></param>
    /// <returns>True if visible</returns>
    private static bool IsOnScreen( Rectangle formRect )
    {
      formRect.Inflate( -20, -20 ); // have to make it a bit smaller as the rectangle is slightly out of screen
      Screen[] screens = Screen.AllScreens;
      foreach ( Screen screen in screens ) {
        if ( screen.WorkingArea.Contains( formRect ) ) {
          return true;
        }
      }
      return false;
    }


    public Form1()
    {
      InitializeComponent( );
    }

    private void Form1_Load( object sender, EventArgs e )
    {
      AppSettings.Instance.Reload( );

      // Assign Size property - check if on screen, else use defaults
      if ( IsOnScreen( new Rectangle( AppSettings.Instance.FormLocation, this.Size ) ) ) {
        this.Location = AppSettings.Instance.FormLocation;
      }

      string host = "192.168.1.69"; uint port = 34123;

      // left side MFDs => MFD_1
      // first the control loaded in the GUI
      MFD_L.InitMFD( MfdInstance.MFD_1, Support.TestConfig1, host, port );
      MFD_L.CommandEvent += MFD_CommandEvent;
      m_leftMfds.Add( MFD_L );
      // create one more and clone parts from the loaded one
      {
        var mfd = new UC_MFDflight { Visible = false, Dock = DockStyle.Fill, Font = MFD_L.Font, BackColor = MFD_L.BackColor };
        tlp.Controls.Add( mfd, 1, 1 ); // add to the Panel Col,Row...
        mfd.InitMFD( MfdInstance.MFD_1, Support.FileAsString( "MFDflight.json" ), host, port );
        mfd.CommandEvent += MFD_CommandEvent;
        m_leftMfds.Add( mfd );
      }
      // create one more and clone parts from the loaded one
      {
        var mfd = new UC_MFDstartLand { Visible = false, Dock = DockStyle.Fill, Font = MFD_L.Font, BackColor = MFD_L.BackColor };
        tlp.Controls.Add( mfd, 1, 1 ); // add to the Panel Col,Row...
        mfd.InitMFD( MfdInstance.MFD_1, Support.FileAsString( "MFDstartLand.json" ), host, port );
        mfd.CommandEvent += MFD_CommandEvent;
        m_leftMfds.Add( mfd );
      }

      // right side MFDs => MFD_2
      // first the control loaded in the GUI
      MFD_R.InitMFD( MfdInstance.MFD_2, Support.TestConfig1, host, port );
      MFD_R.CommandEvent += MFD_CommandEvent;
      m_rightMfds.Add( MFD_R );
      // create one more and clone parts from the loaded one
      {
        var mfd = new UC_MFDflight { Visible = false, Dock = DockStyle.Fill, Font = MFD_R.Font, BackColor = MFD_R.BackColor };
        tlp.Controls.Add( mfd, 3, 1 ); // add to the Panel Col,Row...
        mfd.InitMFD( MfdInstance.MFD_2, Support.FileAsString( "MFDflight.json" ), host, port );
        mfd.CommandEvent += MFD_CommandEvent;
        m_rightMfds.Add( mfd );
      }
      // create one more and clone parts from the loaded one
      {
        var mfd = new UC_MFDstartLand { Visible = false, Dock = DockStyle.Fill, Font = MFD_R.Font, BackColor = MFD_R.BackColor };
        tlp.Controls.Add( mfd, 3, 1 ); // add to the Panel Col,Row...
        mfd.InitMFD( MfdInstance.MFD_2, Support.FileAsString( "MFDstartLand.json" ), host, port );
        mfd.CommandEvent += MFD_CommandEvent;
        m_rightMfds.Add( mfd );
      }

      m_leftMfds.GetNext( );
      m_rightMfds.GetNext( );

      timer1.Start( );
    }

    private void Form1_FormClosing( object sender, FormClosingEventArgs e )
    {
      timer1.Stop( );
      // drop dxInput resources
      foreach ( var uc in m_leftMfds.UserControls ) {
        uc.Shutdown( );
      }
      foreach ( var uc in m_rightMfds.UserControls ) {
        uc.Shutdown( );
      }

      // don't record minimized, maximized forms
      if ( this.WindowState == FormWindowState.Normal ) {
        AppSettings.Instance.FormLocation = this.Location;
      }
      AppSettings.Instance.Save( );
    }

    #endregion

    /// <summary>
    /// Udpate the GUI from a Cmd item
    /// </summary>
    /// <param name="cmd">A Cmd item received from MFD handlers</param>
    private void HandleGUIEvents()
    {
      // process collected commands one after the other
      while ( m_cmdQueue.Count > 0 ) {
        var cmd = m_cmdQueue.Dequeue( );
        // signal button press/release with a slight color change
        pnlSignal.BackColor = ( cmd.Pressed ) ? Color.MediumBlue : Color.Navy;

        // Switch MFDs Commands
        if ( cmd.Command == "NextMFD" ) {
          if ( cmd.Mfd == MfdInstance.MFD_1 )
            m_leftMfds.GetNext( );
          else
            m_rightMfds.GetNext( );
        }
        else if ( cmd.Command == "PrevMFD" ) {
          if ( cmd.Mfd == MfdInstance.MFD_1 )
            m_leftMfds.GetPrev( );
          else
            m_rightMfds.GetPrev( );
        }
        // Other commands
        else {
          ;
        }
      }
    }


    #region Handle Asynch Command Callbacks for the Owner

    // Handle "Ext3": "cmd"   Commands as Owner commands
    // local command received
    private struct Cmd
    {
      public MfdInstance Mfd;
      public string Command;
      public bool Pressed;
    }
    // Store arrived ones to be processed
    private Queue<Cmd> m_cmdQueue = new Queue<Cmd>( );

    // Receive Asynch from DxInput thread
    private void MFD_CommandEvent( object sender, UC_MFD_CmdEventArgs e )
    {
      m_cmdQueue.Enqueue( new Cmd( ) { Mfd = e.Mfd, Command = e.CommandArgument, Pressed = e.Pressed } ); // push it for pickup.. 
      //this.Invoke( (MethodInvoker)delegate { UpdateGUI( ); } ); // CANNOT USE invoke here, switching mfds does not work properly..
    }

    // Within the GUI thread, 
    //  exec callbacks from dxInput handler
    private void timer1_Tick( object sender, EventArgs e )
    {
      HandleGUIEvents( ); // let the FormThread process the callbacks
    }

    #endregion

  }
}
