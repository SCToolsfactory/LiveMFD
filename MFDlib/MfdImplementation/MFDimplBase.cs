using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFDlib.MfdPanel;

namespace MFDlib.MfdImplementation
{
  /// <summary>
  /// Base Class that Implements code for MFD UserControls
  /// </summary>
  public class MFDimplBase
  {
    protected UserControl m_owner { get; set; } = null;

    /// <summary>
    /// The generic Panel handler
    /// </summary>
    public MfdPanelSupport MFD { get; } = new MfdPanelSupport( );

    /// <summary>
    /// State variable controlling painting etc.
    /// Can only paint if initialized
    /// </summary>
    public bool Initialized { get; protected set; } = false;

    // Should implement 
    //public bool InitMFD( UC_MFDname owner, MfdInstance mfd, string configString, MsgProtocol mode, string ip, int port, Control hwnd )

    /// <summary>
    /// Paint proc; takes care of the conf state of the UC
    /// </summary>
    /// <param name="g">Graphics context</param>
    public void Paint( Graphics g )
    {
      if ( Initialized ) {
        MFD.GProc.Paint( g );
      }
    }


  }
}
