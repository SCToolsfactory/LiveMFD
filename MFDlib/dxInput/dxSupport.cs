using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.DirectInput;

namespace MFDlib.dxInput
{
  internal class dxSupport
  {
    public static DevJoystick[] DevJoysticks = new DevJoystick[2];
    public static DevJoystick MfdDevice(MfdInstance mfd )
    {
      if ( mfd == MfdInstance.MFD_None ) return null;

      return DevJoysticks[(int)mfd];
    }

    /// <summary>
    /// Aquire the DInput joystick devices TM Cougar MFD Panel 1 & 2 
    /// </summary>
    /// <returns></returns>
    private static DevJoystick InitDirectInput(string mfdGuid, Control hwnd )
    {
      // we need a 'real' Window for event handling with DxInput
      Control formHwnd = hwnd;
      do {
        formHwnd = formHwnd.Parent;
      } while ( !( formHwnd is Form ) ); // up until we find the main form

      // Initialize DirectInput
      DevJoystick js = null;
      var directInput = new DirectInput( );
      try {
        // scan the Input for attached devices
        foreach ( DeviceInstance instance in directInput.GetDevices( DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly ) ) {
          if ( instance.ProductGuid == new Guid( mfdGuid ) ) {
            js = new DevJoystick( new Joystick( directInput, instance.InstanceGuid ), formHwnd );
            break; // found
          }
        }
      }
      catch ( Exception ex ) {
        Console.WriteLine( "InitDirectInput failed: \n{0}", ex );
        return null;
      }

      return js;
    }

    /// <summary>
    /// Init access to joysticks 
    ///   if initialized gives access to left and right (MFD 1 and MFD 2)
    /// </summary>
    /// <param name="mfd">Which one to init</param>
    /// <param name="hwnd">A window handle</param>
    /// <returns></returns>
    public static bool Init( MfdInstance mfd, Control hwnd )
    {
      string mfdGuid = "";
      if ( mfd == MfdInstance.MFD_1 ) {
        mfdGuid = DevJoystick.MFD1_GUID;
      }
      else if ( mfd == MfdInstance.MFD_2 ) {
        mfdGuid = DevJoystick.MFD2_GUID;
      }
      else {
        MessageBox.Show( "dxSupport.Init() Invalid MFD instance - program exits now", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information );
        return false;
      }
      if ( DevJoysticks[(int)mfd] != null ) return true; // we have it already..

      // Init X things
      var js = InitDirectInput( mfdGuid, hwnd );
      if ( js == null ) {
        MessageBox.Show( "dxSupport.Init() Initializing DirectXInput failed - program exits now", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information );
        return false;
      }
      DevJoysticks[(int)mfd] = js;
      return js!=null;
    }

  }
}
