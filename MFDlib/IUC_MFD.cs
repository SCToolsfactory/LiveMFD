using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib
{
  /// <summary>
  /// This is to implement by the MFD User Control 
  /// so a WinForm Client can rely on basic business methods to work with
  /// </summary>
  public interface IUC_MFD
  {
    /// <summary>
    /// Cleanup and release Devices
    /// </summary>
    void Shutdown();

    /// <summary>
    /// Button is pressed
    ///  A caller can use this to trigger a Button Press 
    /// </summary>
    /// <param name="button">Button No 1..28</param>
    void ButtonPress( int button );

    /// <summary>
    /// Button is released
    ///  A caller can use this to trigger a Button Release
    /// </summary>
    /// <param name="button">Button No 1..28</param>
    void ButtonRelease( int button );

    /// <summary>
    /// Visible property of the UserControl
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// An Event handler for dxInput callbacks for the MFD implementor
    /// </summary>
    event EventHandler<UC_MFD_CmdEventArgs> CommandEvent;

    /// <summary>
    /// Handles external Refreshes 
    /// also out of the GUI thread
    /// </summary>
    void ExternalRefresh();

  }
}
