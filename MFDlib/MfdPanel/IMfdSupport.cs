using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.MfdPanel
{
  /// <summary>
  /// This is to implement by the MFD UserControl
  /// in order to work with the support library
  /// </summary>
  public interface IMfdSupport
  {
    /// <summary>
    /// Implements a callback from Support 
    /// </summary>
    /// <param name="commandArgument">The Ext3 argiment from the config file</param>
    /// <param name="pressed">True if pressed, False if released</param>
    void Command( MfdInstance mfd, string commandArgument, bool pressed );

    /// <summary>
    /// Implements a callback from Support
    /// </summary>
    /// <param name="cmdIPC">A data update packet from SC API Server (if and when...)</param>
    void SCDataUpdate( int data );

  }
}
