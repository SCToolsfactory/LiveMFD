using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.Drawing
{
  /// <summary>
  /// Interface for Items to be drawn by the Drawing processor
  /// </summary>
  public interface IDrawing
  {
    /// <summary>
    /// A Key for an element
    /// </summary>
    string Key { get; set; }

    /// <summary>
    /// The Paint Method
    /// </summary>
    /// <param name="g"></param>
    void Paint( Graphics g );

  }
}
