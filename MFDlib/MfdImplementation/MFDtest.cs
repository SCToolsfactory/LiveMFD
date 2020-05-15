using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFDlib.Drawing;
using MFDlib.MfdPanel;

namespace MFDlib.MfdImplementation
{
  /// <summary>
  /// Implements code for the UC_MFDtest
  /// Layout, Colors, Fonts etc
  /// </summary>
  internal class MFDtest : MFDimplBase
  {

    /// <summary>
    /// Initialize the MFD
    /// </summary>
    /// <param name="configString">A config string to assign buttons, labels</param>
    /// <param name="ip">The IP address of the SCJoyServer</param>
    /// <param name="port">The first Joystick port</param>
    /// <returns>True if successfull</returns>
    public bool InitMFD( UC_MFDtest owner, MfdInstance mfd, string configString, string ip, uint port )
    {
      m_owner = owner;

      Initialized = MFD.Init( owner, mfd, configString, ip, port );
      if ( !Initialized ) return false;

      // Init Drawing stuff for this panel

      // A frame around the usable area
      MFD.GProc.Drawings.Add( MfdBase.Frame( Color.Green ) );
      // Create all labels with a prototype font and color
      MFD.GProc.Drawings.Add( MfdLabels.LabelList(
        new TextItem( ) {
          Font = m_owner.Font,
          TextBrush = new SolidBrush( Color.LimeGreen ),
          TextBrushActive = null,
          FillBrush = null
        },
        MFD.ButtonLabelList ) );
      // Change individual items..

      // Add more to draw

      return Initialized;
    }


  }
}
