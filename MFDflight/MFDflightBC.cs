using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MFDlib;
using MFDlib.Drawing;
using MFDlib.MfdImplementation;
using MFDlib.MfdPanel;

namespace MFDflight
{
  /// <summary>
  /// Implements code for the UC_MFDflight
  /// Layout, Colors, Fonts etc
  /// </summary>
  class MFDflightBC : MFDimplBase
  {
    /// <summary>
    /// Initialize the MFD
    /// </summary>
    /// <param name="configString">A config string to assign buttons, labels</param>
    /// <param name="ip">The IP address of the SCJoyServer</param>
    /// <param name="port">The first Joystick port</param>
    /// <returns>True if successfull</returns>
    public bool InitMFD( UC_MFDflight owner, MfdInstance mfd, string configString, string ip, uint port )
    {
      m_owner = owner;

      Initialized = MFD.Init( owner, mfd, configString, ip, port );
      if ( !Initialized ) return false;

      // Init Drawing stuff for this panel

      // A frame around the usable area
      MFD.GProc.Drawings.Add( MfdBase.Frame( Color.BlueViolet ) );
      // Create all labels with a prototype font and color
      MFD.GProc.Drawings.Add( MfdLabels.LabelList(
        new TextItem( ) {
          Font = m_owner.Font,    //   the font to use for button labels
          TextBrush = new SolidBrush( Color.Violet ),  // the text label color
          TextBrushActive = new SolidBrush( Color.Fuchsia ),  // the active text label color
          FillBrush = null, // new SolidBrush( ControlPaint.Dark( Color.Fuchsia , 0.7F) ) // the text label background fill (or null)
        },   
          MFD.ButtonLabelList ) );
      // Change individual items..

      // Add more to draw

      return Initialized;
    }
  }
}
