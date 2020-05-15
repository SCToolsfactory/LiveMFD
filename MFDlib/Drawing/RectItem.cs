using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.Drawing
{
  /// <summary>
  /// Rectangle drawing 
  /// </summary>
  public class RectItem : DisplayItem
  {
    /// <summary>
    /// cTor: empty
    /// </summary>
    public RectItem() { }

    /// <summary>
    /// cTor: copy from
    ///  we copy refs and do not create new object other than the subitem list
    /// </summary>
    /// <param name="other">The object to create this from</param>
    public RectItem( RectItem other )
      : base( other )
    {
    }

    /// <summary>
    /// Draw a rectangle 
    ///   with fill  (FillBrush set)
    ///   and  frame (Pen set)
    /// </summary>
    /// <param name="g">Graphics context</param>
    protected override void PaintThis( Graphics g )
    {
      if ( FillBrush != null )
        g.FillRectangle( FillBrush, Rectangle );
      if ( Pen != null )
        g.DrawRectangle( Pen, Rectangle );
    }

  }
}
