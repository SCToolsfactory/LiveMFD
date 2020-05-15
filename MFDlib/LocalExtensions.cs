using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib
{
    /// <summary>
  /// Some helping hands
  /// </summary>
  internal static class LocalExtensions
  {
    /// <summary>
    /// Add a Point to this
    /// </summary>
    /// <param name="source">A Point</param>
    /// <param name="other">The Point to add</param>
    /// <returns>A new Point with added Coordinates</returns>
    public static Point Add( this Point source, Point other )
    {
      return new Point( source.X + other.X, source.Y + other.Y );
    }

    public static Rectangle Offset( this Rectangle source, Size other )
    {
      source.Offset( other.Width, other.Height );
      return source;
    }



  }
}
