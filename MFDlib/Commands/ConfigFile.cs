using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using vjMapper.JInput;
using vjMapper.VjOutput;

namespace MFDlib.Commands
{
  /// <summary>
  /// The Device Mapping File
  /// </summary>
  [DataContract]
  internal class ConfigFile
  {
    /*
        {
          "_Comment" : "Cougar MFD Panel Config File",
          "MapName" : "AnyNameWillDo",
          "BackgroundImage": "an image filename",
          "Macros"   : [MACRO,..],
          "SwitchMap": [
              { "Input": "BT1",       "Cmd" : [ COMMAND_on(press), COMMAND_off(release) ] },
              ...
              { "Input": "BT28",       "Cmd" : [ COMMAND_on(press), COMMAND_off(release) ] }
            ],
          "ButtonLabels": ["bt1",.., "bt28"]
        }

         COMMAND is defined in vjMapping

        REM: 
        Button Input string MUST be unique in the file - serving as Keys for the command entries
        Buttons:  Top Left 1..5, Right Top 6..10, Bottom Right 11..15, Left Bottom 16..20
                   Switch Top    Right 21 (up), 22 (down)
                   Switch Bottom Right 23 (up), 24 (down)
                   Switch Bottom Left  25 (up), 26 (down)
                   Switch Top    Left  27 (up), 28 (down)
        In labels use '¬' to make a line break (don't use \n or other stuff - it breaks..)

      Use Ext3 command attribute for e.g. switching panels
    */

    // public members
    [DataMember( Name = "_Comment" )]    // optional
    public string Comment { get; set; }
    [DataMember( IsRequired = true, Name = "MapName" )]
    public string MapName { get; set; }
    [DataMember( IsRequired = true, Name = "BackgroundImage" )]
    public string BackgroundImage { get; set; }

    [DataMember( Name = "ButtonLabels" )]  // optional
    private List<string> ButtonLabels { get; set; } = new List<string>( ); // button 1 gets index 0...

    // private members
    [DataMember( Name = "Macros" )]     // optional
    private MacroDefList Macros { get; set; } = new MacroDefList( );
    [DataMember( Name = "SwitchMap" )]  // optional
    private InputSwitchList SwitchMap { get; set; } = new InputSwitchList( );

    // non Json

    private Dictionary<string, string> m_labelCache = null;
    /// <summary>
    /// A Dictionary of labels where the key is the Input Key and the Value the Label string
    /// </summary>
    public Dictionary<string, string> Labels
    {
      get {
        if ( m_labelCache == null ) {
          int index = 0;
          m_labelCache = new Dictionary<string, string>( );
          foreach ( var s in ButtonLabels ) {
            if ( index < SwitchMap.Count ) { // in case labels mismatch the inputs
              m_labelCache.Add( SwitchMap[index++].Input, s.Replace( "¬", $"\n" ) );
            }
          }
        }
        return m_labelCache;
      }
    }

    /// <summary>
    /// Return the dictionary of commands 
    /// </summary>
    /// <returns>A Dictionary with Input NAME as Key and the corresponding VJCommand</returns>
    public VJCommandDict VJCommandDict
    {
      get {
        // combine all commands
        var ret = SwitchMap.VJCommandDict( Macros );    // get switches

        return ret; // all collected commands
      }
    }

    /// <summary>
    /// Return the list of macros
    /// </summary>
    /// <returns>A List of MacroDefs</returns>
    public MacroDefList VJMacros { get => Macros; }

  }
}
