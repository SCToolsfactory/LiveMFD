using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MFDlib.MfdPanel.MfdGeo;
using MFDlib.Drawing;

namespace MFDlib.MfdPanel
{


  /// <summary>
  /// Helper to create a Label TextItem and it's label items as sublist
  /// </summary>
  public class MfdLabels
  {
    private readonly static Size c_rowType = new Size( 63, 40 ); // row type width,height
    private readonly static Size c_colType = new Size( 100, 53 ); // column type width,height
    private readonly static Size c_cswType = new Size( 35, 60 ); // switch type (corners) width,height


    // The Master items are in MfdConsts

    // where to paint the switch label
    private readonly static Rectangle c_topLeftSwitch = new Rectangle( MfdTextTopLeft, c_cswType );
    private readonly static Rectangle c_topRightSwitch = new Rectangle( MfdTextTopRight.Add( new Point( -c_cswType.Width, 0 ) ), c_cswType );
    private readonly static Rectangle c_bottomLeftSwitch = new Rectangle( MfdTextBottomLeft.Add( new Point( 0, -c_cswType.Height ) ), c_cswType );
    private readonly static Rectangle c_bottomRightSwitch = new Rectangle( MfdTextBottomRight.Add( new Point( -c_cswType.Width, -c_cswType.Height ) ), c_cswType );

    private readonly static Rectangle c_topLeftRow = new Rectangle( c_topLeftSwitch.Location.Add( new Point( c_cswType.Width, 0 ) ), c_rowType ); // start
    private readonly static Rectangle c_topRightCol = new Rectangle( MfdTextTopRight.Add( new Point( -c_colType.Width, c_cswType.Height ) ), c_colType ); // start
    private readonly static Rectangle c_bottomRightRow = new Rectangle( MfdTextBottomRight.Add( new Point( -c_cswType.Width - c_rowType.Width, -c_rowType.Height ) ), c_rowType );
    private readonly static Rectangle c_bottomLeftCol = new Rectangle( MfdTextBottomLeft.Add( new Point( 0, -c_cswType.Height - c_colType.Height ) ), c_colType );

    private const string c_LabelKey = "LABELS";

    /// <summary>
    /// Creates all text fields for an MFD panel (28 items)
    /// Copies the display attributes from the prototype TextItem
    /// </summary>
    /// <param name="labels">The list of labels to print (takes from the beginning as long as it lasts if not 28 provided)</param>
    /// <returns>A TextItem("LABELS") containing all labels as SubItemList</returns>
    public static TextItem LabelList( TextItem prototype, Dictionary<string, string> labels )
    {
      var list = new DisplayList( );
      // create an item that just carries the sublist of text items
      var ret = new TextItem( ){
        Key = c_LabelKey, String = "", // no show item
      };

      TextItem item = null; // sub item to add
      //setup the label rectangles
      for ( int i = 0; i < labels.Count; i++ ) {
        item = prototype.Clone( );
        item.Key = labels.ElementAt( i ).Key;
        item.String = labels.ElementAt( i ).Value;
        item.Rectangle = MfdLabelRect[i];
        item.StringFormat.Alignment = MfdLabelStringAlignment[i];
        list.AddItem( item );
      }

      // finally add the stuff and return
      ret.SubItemList.AddRange( list );
      return ret;
    }


  }
}
