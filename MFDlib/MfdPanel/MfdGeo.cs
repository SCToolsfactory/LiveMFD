using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.MfdPanel
{
  /// <summary>
  /// Panel Gometry consts
  /// </summary>
  public static class MfdGeo
  {

    /// <summary>
    /// The MFD painting Area relative to the UC_XY Control
    ///  as we paint on that control
    /// </summary>
    public readonly static Rectangle MfdArea = new Rectangle( 20, 29, 386, 389 );
    // useful points
    public readonly static Point MfdAreaTopLeft = MfdArea.Location; // top left of all painting
    public readonly static Point MfdAreaTopRight = new Point( MfdArea.Right, MfdArea.Top );
    public readonly static Point MfdAreaBottomLeft = new Point( MfdArea.Left, MfdArea.Bottom );
    public readonly static Point MfdAreaBottomRight = new Point( MfdArea.Right, MfdArea.Bottom );

    // The text area within the MfdArea (-2px inflated)
    public readonly static Rectangle MfdText = new Rectangle( MfdArea.Left + 2, MfdArea.Top + 2, MfdArea.Width - 4, MfdArea.Height - 4 );
    // useful points
    public readonly static Point MfdTextTopLeft = MfdText.Location; // top left of all texting
    public readonly static Point MfdTextTopRight = new Point( MfdText.Right, MfdText.Top );
    public readonly static Point MfdTextBottomLeft = new Point( MfdText.Left, MfdText.Bottom );
    public readonly static Point MfdTextBottomRight = new Point( MfdText.Right, MfdText.Bottom );

    /// <summary>
    /// Label Rectangles created on init
    /// Bt 1..28 -> Label[0]..Label[27]
    /// </summary>
    public readonly static List<Rectangle> MfdLabelRect = new List<Rectangle>( );
    /// <summary>
    /// Prefab. String alignment for the MFD labels
    /// </summary>
    public readonly static List<StringAlignment> MfdLabelStringAlignment = new List<StringAlignment>( );

    //  private helpers
    // Label outlines for MY screen
    private readonly static Size c_rowType = new Size( 63, 40 ); // row type width,height
    private readonly static Size c_colType = new Size( 100, 53 ); // column type width,height
    private readonly static Size c_cswType = new Size( 35, 60 ); // switch type (corners) width,height

    // where to paint the switch label - before row and col labels (dependency on init..)
    private readonly static Rectangle c_topLeftSwitch = new Rectangle( MfdTextTopLeft, c_cswType );
    private readonly static Rectangle c_topRightSwitch = new Rectangle( MfdTextTopRight.Add( new Point( -c_cswType.Width, 0 ) ), c_cswType );
    private readonly static Rectangle c_bottomLeftSwitch = new Rectangle( MfdTextBottomLeft.Add( new Point( 0, -c_cswType.Height ) ), c_cswType );
    private readonly static Rectangle c_bottomRightSwitch = new Rectangle( MfdTextBottomRight.Add( new Point( -c_cswType.Width, -c_cswType.Height ) ), c_cswType );

    // where to paint the row and col labels
    private readonly static Rectangle c_topLeftRow = new Rectangle( c_topLeftSwitch.Location.Add( new Point( c_cswType.Width, 0 ) ), c_rowType ); // start
    private readonly static Rectangle c_topRightCol = new Rectangle( MfdTextTopRight.Add( new Point( -c_colType.Width, c_cswType.Height ) ), c_colType ); // start
    private readonly static Rectangle c_bottomRightRow = new Rectangle( MfdTextBottomRight.Add( new Point( -c_cswType.Width - c_rowType.Width, -c_rowType.Height ) ), c_rowType );
    private readonly static Rectangle c_bottomLeftCol = new Rectangle( MfdTextBottomLeft.Add( new Point( 0, -c_cswType.Height - c_colType.Height ) ), c_colType );

    /// <summary>
    /// The user area not touching the label area
    /// </summary>
    public readonly static Rectangle MfdUser = new Rectangle( c_bottomLeftCol.Right + 2, c_topLeftRow.Bottom + 2,
                                                            c_topRightCol.Left - c_bottomLeftCol.Right - 4, c_bottomRightRow.Top - c_topLeftRow.Bottom - 4 );




    /// <summary>
    /// cTor for the static class
    /// </summary>
    static MfdGeo()
    {
      //setup the label rectangles bt1..28 -> labelRect 0..27
      int i;
      var r = c_topLeftRow;
      for ( i = 0; i < 5; i++ ) {
        // bt 1..5 row top left to right
        MfdLabelRect.Add( r );
        MfdLabelStringAlignment.Add( StringAlignment.Center );
        r.Offset( c_rowType.Width, 0 );
      }
      r = c_topRightCol;
      for ( i = 0; i < 5; i++ ) {
        // bt 6..10 col right top to bottom
        MfdLabelRect.Add( r );
        MfdLabelStringAlignment.Add( StringAlignment.Far );
        r.Offset( 0, c_colType.Height );
      }
      r = c_bottomRightRow;
      for ( i = 0; i < 5; i++ ) {
        // bt 11..15 col bottom right to left
        MfdLabelRect.Add( r );
        MfdLabelStringAlignment.Add( StringAlignment.Center );
        r.Offset( -c_rowType.Width, 0 );
      }
      r = c_bottomLeftCol;
      for ( i = 0; i < 5; i++ ) {
        // bt 16..20 col left bottom to top
        MfdLabelRect.Add( r );
        MfdLabelStringAlignment.Add( StringAlignment.Near );
        r.Offset( 0, -c_colType.Height );
      }

      // bt 21 switch top right up
      MfdLabelRect.Add( c_topRightSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Far );
      // bt 22 switch top right down
      MfdLabelRect.Add( c_topRightSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Far );

      // bt 23 switch bottom right up
      MfdLabelRect.Add( c_bottomRightSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Far );
      // bt 24 switch bottom right down
      MfdLabelRect.Add( c_bottomRightSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Far );

      // bt 25 switch bottom left up
      MfdLabelRect.Add( c_bottomLeftSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Near );
      // bt 26 switch bottom left down
      MfdLabelRect.Add( c_bottomLeftSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Near );

      // bt 27 switch top left up
      MfdLabelRect.Add( c_topLeftSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Near );
      // bt 28 switch top left down
      MfdLabelRect.Add( c_topLeftSwitch );
      MfdLabelStringAlignment.Add( StringAlignment.Near );

    }



  }
}
