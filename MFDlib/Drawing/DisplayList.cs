using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.Drawing
{
  /// <summary>
  /// The list of items to paint
  /// </summary>
  public class DisplayList : List<IDrawing>
  {

    /// <summary>
    /// Add a DisplayItem to the DisplayList
    /// </summary>
    /// <param name="item"></param>
    public void AddItem( DisplayItem item )
    {
      this.Add( item );
    }

    /// <summary>
    /// Remove a DisplayItem with key
    /// </summary>
    /// <param name="key">The key</param>
    public void RemoveItem( string key )
    {
      // to remove safely we start from the back..
      for ( int i = this.Count - 1; i >= 0; i-- ) {
        if ( this[i].Key == key ) {
          this.RemoveAt( i );
        }
      }
    }

    /// <summary>
    /// Returns the DisplayItem with Key (first found)
    /// </summary>
    /// <param name="key">The key</param>
    /// <returns>A DisplayItem or null</returns>
    public DisplayItem DispItem( string key )
    {
      foreach ( var di in this ) {
        if (di is DisplayItem ) {
          var dItem = di as DisplayItem;
          if ( di.Key == key )  return dItem;
          // try the subitems
          dItem = dItem.SubItemList.DispItem( key );
          if ( dItem != null ) return dItem;
        }
      }
      return null;
    }

    /// <summary>
    /// Deactivates all DisplayItems
    /// </summary>
    public  void DeactivateAllDisplayItems()
    {
      foreach ( var di in this ) {
        if ( di is DisplayItem ) {
          var dItem = di as DisplayItem;
          dItem.Pressed = false;
          dItem.SubItemList.DeactivateAllDisplayItems( );
        }
      }
    }

    /// <summary>
    /// Does all paints 
    /// </summary>
    /// <param name="g">Graphics context</param>
    public void Paint(Graphics g )
    {
      foreach(var i in this ) {
        i.Paint( g );
      }
    }


  }
}
