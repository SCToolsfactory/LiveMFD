using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

using SharpDX;
using SharpDX.DirectInput;
using System.Threading;

namespace MFDlib.dxInput
{
  /// <summary>
  /// Handles one JS device as DXInput device
  /// In addition provide some static tools to handle JS props here in one place
  /// Also owns the GUI i.e. the user control that shows all values
  /// 
  /// 	joystick: F16 MFD 1 	 - Product: {B351044F-0000-0000-0000-504944564944} - Instance: {d0d6ed60-7391-11ea-8001-444553540000}
  /// 	joystick: F16 MFD 2 	 - Product: {B352044F-0000-0000-0000-504944564944} - Instance: {d0d71470-7391-11ea-8002-444553540000}
  /// </summary>
  internal class DevJoystick 
  {
    //    private static readonly log4net.ILog log = log4net.LogManager.GetLogger( MethodBase.GetCurrentMethod( ).DeclaringType );

    #region Static Items

    static public int RegisteredDevices = 0;

    static public string MFD1_GUID = "{B351044F-0000-0000-0000-504944564944}"; // product GUID
    static public string MFD2_GUID = "{B352044F-0000-0000-0000-504944564944}"; // product GUID

    #endregion

    // ****************** CLASS *************************

    private SharpDX.DirectInput.Joystick m_device;

    private JoystickState m_state = new JoystickState( );
    private JoystickState m_prevState = new JoystickState( );

    private Control m_hwnd;

    /// <summary>
    /// The JS ProductName property
    /// </summary>
    public string DevName { get { return m_device.Properties.ProductName; } }
    /// <summary>
    /// The ProductGUID property
    /// </summary>
    public string DevGUID { get { return "{" + m_device.Information.ProductGuid.ToString( ).ToUpperInvariant( ) + "}"; } }
    /// <summary>
    /// The JS Instance GUID for multiple device support (VJoy gets 2 of the same name)
    /// </summary>
    public string DevInstanceGUID { get { return m_device.Information.InstanceGuid.ToString( ); } }

    // device props
    public int AxisCount { get { return m_device.Capabilities.AxeCount; } }
    public int ButtonCount { get { return m_device.Capabilities.ButtonCount; } }
    public int POVCount { get { return m_device.Capabilities.PovCount; } }

    /// <summary>
    /// ctor and init
    /// </summary>
    /// <param name="device">A DXInput device</param>
    /// <param name="hwnd">The WinHandle of the main window</param>
    /// <param name="joystickNum">The 0.. n-1 Joystick from DX enum</param>
    public DevJoystick( SharpDX.DirectInput.Joystick device, Control hwnd )
    {
      //      log.DebugFormat( "JoystickCls ctor - Entry with {0}", device.Information.ProductName );

      m_device = device;
      m_hwnd = hwnd;
      // Set BufferSize in order to use buffered data.
      m_device.Properties.BufferSize = 128;

      //      log.Debug( "Get JS Objects" );
      try {
        // Set the data format to the c_dfDIJoystick pre-defined format.
        //m_device.SetDataFormat( DeviceDataFormat.Joystick );
        // Set the cooperative level for the device.
        m_device.SetCooperativeLevel( m_hwnd, CooperativeLevel.NonExclusive | CooperativeLevel.Background );
        // Enumerate all the objects on the device.
        foreach ( DeviceObjectInstance d in m_device.GetObjects( ) ) {
          // For axes that are returned, set the DIPROP_RANGE property for the
          // enumerated axis in order to scale min/max values.
          if ( ( 0 != ( d.ObjectId.Flags & DeviceObjectTypeFlags.Axis ) ) ) {
            // Set the range for the axis.
            m_device.Properties.Range = new InputRange( -1000, +1000 );
          }

        }
        StartEventHandling( );
      }
      catch ( Exception ex ) {
        Console.WriteLine( "Get JS Objects failed:\n{0}", ex );
      }
      DevJoystick.RegisteredDevices++;
    }



    /// <summary>
    /// Shutdown device access
    /// </summary>
    public void FinishDX()
    {
        StopEventHandling( );
    }

    /// <summary>
    /// returns the currently available input string
    ///  (does not retrieve new data but uses what was collected by GetData())
    ///  NOTE: for Joystick when multiple inputs are available the sequence is 
    ///    axis > button > hat > slider (wher prio is max itemNum > min itemNum)
    /// </summary>
    /// <returns>An input string or an empty string if no input is available</returns>
    private void EvalCurrentInput()
    {

      // get prio button
      bool[] buttons = m_state.Buttons;
      bool[] prevButtons = m_prevState.Buttons;
      //pressed ones
      for ( int bi = 0; bi < buttons.Length; bi++ ) {
        bool newPress = true;
        for ( int bp = 0; bp < prevButtons.Length; bp++ ) {
          //previously pressed ones
          if ( prevButtons[bp] == buttons[bi] )
            newPress = false; // not a new press
        }
        if ( newPress )
          OnButtonDown( bi + 1 );
      }

      //released ones
      for ( int bp = 0; bp < prevButtons.Length; bp++ ) {
        bool newRelease = true;
        for ( int bi = 0; bi < buttons.Length; bi++ ) {
          //previously pressed ones
          if ( prevButtons[bp] == buttons[bi] )
            newRelease = false; // not a new release
        }
        if ( newRelease )
          OnButtonUp( bp + 1 );
      }
    }


    /// <summary>
    /// Collect the current data from the device
    /// </summary>
    private void GetData()
    {
      // Make sure there is a valid device.
      if ( null == m_device )
        return;

      // Poll the device for info.
      try {
        m_device.Poll( );
      }
      catch ( SharpDXException e ) {
        if ( ( e.ResultCode == ResultCode.NotAcquired ) || ( e.ResultCode == ResultCode.InputLost ) ) {
          // Check to see if either the app needs to acquire the device, or
          // if the app lost the device to another process.
          try {
            // Acquire the device
            m_device.Acquire( );
          }
          catch ( SharpDXException ) {
            // Failed to acquire the device. This could be because the app doesn't have focus.
            return;  // EXIT unaquired
          }
        }
        else {
          //          log.Error( "Unexpected Poll Exception", e );
          return;  // EXIT see ex code
        }
      }


      // Get the state of the device - retaining the previous state to find the lates change
      m_prevState = m_state;
      try { m_state = m_device.GetCurrentState( ); }
      // Catch any exceptions. None will be handled here, 
      // any device re-aquisition will be handled above.  
      catch ( SharpDXException ) {
        return;
      }
    }



    #region Event Handler

    /// <summary>
    /// Occurs when a Joystick Button is pressed
    /// </summary>
    public event EventHandler<DevJoystickButtonEventArgs> ButtonDown;

    /// <summary>
    /// Occurs when a Joystick Button is released
    /// </summary>
    public event EventHandler<DevJoystickButtonEventArgs> ButtonUp;

    // trigger callbacks
    private void OnButtonDown( int buttonNo )
    {
      ButtonDown?.Invoke( this, new DevJoystickButtonEventArgs( buttonNo ) );
    }

    private void OnButtonUp( int buttonNo )
    {
      ButtonUp?.Invoke( this, new DevJoystickButtonEventArgs( buttonNo ) );
    }


    private WaitHandle m_joystickNotification = new EventWaitHandle( false, EventResetMode.AutoReset);

    private Thread m_thread = null;
    private bool m_abort = false;

    /// <summary>
    /// Start event handling
    /// </summary>
    public void StartEventHandling()
    {
      if ( ( m_thread != null ) && m_thread.IsAlive ) return; // running ..
      m_prevState = new JoystickState( ); // reset before starting
      m_device.SetNotification( m_joystickNotification );
      m_device.Acquire( );
      m_abort = false;
      // start Thread
      m_thread = new Thread( new ThreadStart( TaskLoop ) );
      m_thread.Start( );
    }


    public void StopEventHandling()
    {
      m_abort = true;
      if ( ( m_thread != null ) && m_thread.IsAlive ) {
        m_thread.Abort( );
      }
      if (m_device != null ) {
        if ( m_device.IsDisposed ) return;

        m_device.Unacquire( );
        m_device.SetNotification( null );
      }
    }

    private void TaskLoop()
    {
      while ( !m_abort ) {
        m_joystickNotification.WaitOne( );
        // do something
        GetData( );
        EvalCurrentInput( );

      }
      m_joystickNotification.Close( );
    }


    #endregion




  }
}
