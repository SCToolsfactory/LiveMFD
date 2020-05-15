﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.Drawing
{
  public enum ActiveState
  {
    Off=0,
    Armed,
    Engaged,
  }

  /// <summary>
  /// BaseClass to implement items to draw
  /// supports sub items and a Key to manipulate from the DisplayList
  /// provide some basic features for drawing
  /// 
  /// </summary>
  public abstract class DisplayItem : IDrawing
  {

    /// <summary>
    /// Access the SubItem List (a DisplayList)
    /// Manipulate elements via this property only
    /// </summary>
    public DisplayList SubItemList { get; } = new DisplayList( );

    /// <summary>
    /// A key to access this item in the DisplayList
    /// </summary>
    public string Key { get; set; } = "";

    /// <summary>
    /// Indicates if this item is pressed
    /// Can be used to change the paint behavior for active and inactive items
    /// </summary>
    public bool Pressed { get; set; } = false;

    /// <summary>
    /// Indicates if this item is active 
    /// Can be used to change the paint behavior for active and inactive items
    /// </summary>
    public ActiveState Active { get; set; } =  ActiveState.Off;


    //  Basic Drawing properties one can use

    /// <summary>
    /// The outer bounds for the item
    /// </summary>
    public Rectangle Rectangle { get; set; } = new Rectangle( );
    /// <summary>
    /// A Pen to use for lines
    /// </summary>
    public Pen Pen { get; set; } = Pens.Pink; // default Pen (alarm color if not set)
    /// <summary>
    /// A Brush to use for Fills
    /// </summary>
    public Brush FillBrush { get; set; } = Brushes.Pink; // default Brush (alarm color if not set)

    /// <summary>
    /// A Brush to use for Fills
    /// </summary>
    public Brush BackgBrushArmed { get; set; } = Brushes.Pink; // default Brush (alarm color if not set)

    /// <summary>
    /// A Brush to use for Fills
    /// </summary>
    public Brush BackgBrushEngaged { get; set; } = Brushes.Pink; // default Brush (alarm color if not set)

    /// <summary>
    /// A Brush to use for Text
    /// </summary>
    public Brush TextBrush { get; set; } = Brushes.Pink; // default Brush (alarm color if not set)

    /// <summary>
    /// A Brush to use for Text Active
    /// </summary>
    public Brush TextBrushActive { get; set; } = Brushes.Pink; // default Brush (alarm color if not set)

    /// <summary>
    /// A font to use for text
    /// </summary>
    public Font Font { get; set; } = SystemFonts.DialogFont;
    /// <summary>
    /// A string to use for text
    /// </summary>
    public string String { get; set; } = "undef";


    /// <summary>
    /// cTor: empty
    /// </summary>
    public DisplayItem() { }

    /// <summary>
    /// cTor: copy from
    ///  we copy refs and do not create new object other than the subitem list
    /// </summary>
    /// <param name="other">The object to create this from</param>
    public DisplayItem( DisplayItem other )
    {
      this.Pressed = other.Pressed;
      this.Active = other.Active;
      this.FillBrush = other.FillBrush;
      this.BackgBrushArmed = other.BackgBrushArmed;
      this.BackgBrushEngaged = other.BackgBrushEngaged;
      this.Font = other.Font;
      this.Key = other.Key;
      this.Pen = other.Pen;
      this.Rectangle = other.Rectangle; // don't know struct may be copied as value
      this.String = other.String;
      this.SubItemList.AddRange( other.SubItemList );
      this.TextBrush = other.TextBrush;
      this.TextBrushActive = other.TextBrushActive;
    }

    /// <summary>
    /// Draw all of this item
    /// </summary>
    /// <param name="g">Graphics context</param>
    public void Paint( Graphics g )
    {
      SubItemList.Paint( g ); // paint all below this item
      PaintThis( g ); // paint this item
    }

    /// <summary>
    /// Paint this item
    /// Override in the implementation as needed (base does not do anything)
    /// </summary>
    /// <param name="g">Graphics context</param>
    protected virtual void PaintThis( Graphics g ) { }

  }
}
