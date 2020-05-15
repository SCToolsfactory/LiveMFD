using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFDlib.Commands;
using MFDlib.Drawing;
using MFDlib.dxInput;
using MFDlib.OwnerSupport;
using MFDlib.Protocol;
using vjMapper.JInput;

namespace MFDlib.MfdPanel
{

  /// <summary>
  /// Operational support for MFD panel handling
  /// </summary>
  public class MfdPanelSupport
  {

    private MfdPanelConfig m_panelConfig = null; // the configuration object
    private UdpMessenger m_udp = null;           // the UDP messenger to send commands with
    private IMfdSupport m_uc = null;             // the UserControl Interface for the callbacks on button press
    private IUC_MFD m_mfd = null;                // the UserControl Interface for the callbacks on button press
    private bool m_active = false;

    /// <summary>
    /// Error message for failed calls
    /// </summary>
    public string ErrorMsg { get; private set; } = "";

    /// <summary>
    /// The graphics processor
    /// </summary>
    public GProc GProc { get; } = new GProc( );// just one per instance

    /// <summary>
    /// The graphics processor
    /// </summary>
    public MacroDefList ConfigMacros { get => m_panelConfig.VJMacros; }

    /// <summary>
    /// The instance of this MFD panel
    /// </summary>
    public MfdInstance MfdInstance { get; private set; } = MfdInstance.MFD_None;

    /// <summary>
    /// The MFD Panel Device (a DevJoystick)
    /// </summary>
    internal DevJoystick MfdDevice { get; private set; } = null;

    /// <summary>
    /// Init this Panel
    /// </summary>
    /// <param name="uc">The user control</param>
    /// <param name="mfd">The MFD instance</param>
    /// <param name="configString">A config string</param>
    /// <param name="ip">The IP address of the SCJoyServer</param>
    /// <param name="port">The Server base port</param>
    public bool Init( IMfdSupport uc, MfdInstance mfd, string configString, string ip, uint port )
    {
      m_uc = uc; // save this one to trigger callbacks
      m_mfd = uc as IUC_MFD;

      // Get the devices - else everything else is of no value
      if ( !dxSupport.Init( mfd, m_uc as Control ) ) {
        ErrorMsg = $" Cannot access MFD device - aborting";
        return false;
      }
      MfdDevice = dxSupport.MfdDevice( mfd );

      // Try to load the config file
      m_panelConfig = MfdPanelConfig.FromJsonString( configString );
      if ( !m_panelConfig.Valid ) {
        ErrorMsg = $" Invalid Config File - aborting\n  msg: {MfdPanelConfig.ErrorMsg}";
        return false;
      }

      // Try to setup an UDP messenger to connect to JoystickServer
      if ( UdpMessenger.CheckIP( ip ) ) {
        m_udp = new UdpMessenger( ip, port ); // use the main port, it handles the JNo properly
      }
      else {
        ErrorMsg = $"SwitchPanelSupport - Invalid IP # {ip}";
        return false;
      }
      // set a valid instance
      MfdInstance = mfd;

      // attach button handlers from DxInput
      MfdDevice.ButtonDown += MfdDevice_ButtonDown;
      MfdDevice.ButtonUp += MfdDevice_ButtonUp;
      return true;
    }


    /// <summary>
    /// Shutdown if DXInput
    /// </summary>
    public void Shutdown()
    {
      MfdDevice?.FinishDX( );
    }

    /// <summary>
    /// Returns the configfile defined background image
    /// </summary>
    public Image BackgroundImage
    {
      get {
        string fName = Support.FileLocator( m_panelConfig.BackgroundImageFile );
        try {
          return Image.FromFile( fName );
        }
        catch {
          return Properties.Resources.backg_notdefined;
        }
      }
    }


    /// <summary>
    /// The dictionary of all labels from the config
    /// Key is the Input label for each button
    /// </summary>
    public Dictionary<string, string> ButtonLabelList { get => m_panelConfig.Labels; }

    /// <summary>
    /// Return the label for a button with number
    /// </summary>
    /// <param name="button">Button No 1..28</param>
    /// <returns>A string</returns>
    public string ButtonLabel( int button )
    {
      return m_panelConfig.ButtonLabel( button );
    }

    /// <summary>
    /// Enable/Disable DxInput Capture
    /// </summary>
    /// <param name="active"></param>
    public void ActivateDxInput( bool active )
    {
      m_active = active;
    }

    /// <summary>
    /// Deactivates all DisplayItems
    /// </summary>
    public void DeactivateAllDisplayItems()
    {
      GProc.Drawings.DeactivateAllDisplayItems( );
    }

    // Send to either of the Messengers
    public void SendMsg( vjMapper.VjOutput.VJCommand cmd )
    {
      m_udp?.SendMsg( cmd );
    }

    // final button handler
    private void HandleButtonKey( int button, bool pressed )
    {
      if ( !m_active ) return;

      // get the proper key name from the input
      //  first from our config file and then wrap it with the vjMapper Switch Handler
      // gets something like BT3_on or BT9_off to find the assoc command
      string bKey = m_panelConfig.ButtonKey( button ); // button key
      string cKey = ""; // command key
      if ( pressed ) {
        cKey = InputSwitch.Input_On( bKey );
      }
      else {
        cKey = InputSwitch.Input_Off( bKey );
      }

      // Console.WriteLine( $"MFD {MfdInstance} - Button  {key}" );

      // try to activate the label if we find it
      var di = GProc.Drawings.DispItem( bKey );
      if ( di != null ) {
        di.Pressed = pressed;
        m_mfd.ExternalRefresh( );
      }


      // bail out on not founds..
      if ( !m_panelConfig.VJCommands.ContainsKey( cKey ) ) {
        // callback to owner in any case to signal a button press/release
        m_uc.Command( MfdInstance, "", pressed );
        return;
      }

      // get the assoc command of the key (if found)
      var cmd = m_panelConfig.VJCommands[cKey];
      // bail out on invalid cmds i.e. not found
      if ( !cmd.IsValid ) {
        // callback to owner in any case to signal a button press/release
        m_uc.Command( MfdInstance, "", pressed );
        return;
      }

      // valid cmd found 

      // callback to owner with valid Cmd (may be an Ext3 .. or empty)
      m_uc.Command( MfdInstance, cmd.CtrlExt3, pressed );
      // send it out
      SendMsg( cmd );
    }

    /// <summary>
    /// Trigger a Button is pressed
    /// </summary>
    /// <param name="button"></param>
    public void ButtonPress( int button )
    {
      HandleButtonKey( button, true );
    }

    /// <summary>
    /// Trigger a Button is released
    /// </summary>
    /// <param name="button"></param>
    public void ButtonRelease( int button )
    {
      HandleButtonKey( button, false );
    }

    // DxInput attached handler
    private void MfdDevice_ButtonUp( object sender, DevJoystickButtonEventArgs e )
    {
      ButtonRelease( e.ButtonNo );
    }

    // DxInput attached handler
    private void MfdDevice_ButtonDown( object sender, DevJoystickButtonEventArgs e )
    {
      ButtonPress( e.ButtonNo );
    }

  }
}
