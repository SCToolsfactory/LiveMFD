using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MFDlib.Drawing;

namespace MFDlib.MfdPanel
{
  /// <summary>
  /// Provides some Base items to draw 
  /// </summary>
  public class MfdBase
  {

    public static RectItem Frame(Color color)
    {
      var r = new RectItem( ) {Key="FRAME",
        Rectangle = MfdGeo.MfdArea, FillBrush = null, Pen = new Pen( color )
      };
      return r;
    }

  }
}
