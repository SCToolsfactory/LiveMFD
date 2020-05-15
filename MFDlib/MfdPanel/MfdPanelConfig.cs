using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFDlib.Commands;
using vjMapper;
using vjMapper.JInput;
using vjMapper.VjOutput;

namespace MFDlib.MfdPanel
{
  /// <summary>
  /// Configuration of the MFD Panel
  ///  this is feed with a JSON file 
  ///  
  /// </summary>
  internal class MfdPanelConfig
  {
    /// <summary>
    /// Provides error information if we get an invalis object
    /// </summary>
    public static string ErrorMsg { get; private set; } = "";

    /// <summary>
    /// Reads from the open stream and returns a MfdPanelConfig
    /// </summary>
    /// <param name="jStream">An open stream at position</param>
    /// <returns>A MfdPanelConfig obj</returns>
    public static MfdPanelConfig FromJson( Stream jStream )
    {
      var c = vjMapping.FromJsonStream<ConfigFile>( jStream );
      if ( c == default( ConfigFile ) ) {
        ErrorMsg = vjMapping.ErrorMsg;
      }
      return new MfdPanelConfig( c );
    }

    /// <summary>
    /// Reads from a file and returns a MfdPanelConfig
    /// </summary>
    /// <param name="jFilename">The Json Filename</param>
    /// <returns>A MfdPanelConfig obj</returns>
    public static MfdPanelConfig FromJson( string jFilename )
    {
      var c = vjMapping.FromJsonFile<ConfigFile>( jFilename );
      if ( c == default( ConfigFile ) ) {
        ErrorMsg = vjMapping.ErrorMsg;
      }
      return new MfdPanelConfig( c );
    }

    /// <summary>
    /// Reads from a string and returns a MfdPanelConfig
    /// </summary>
    /// <param name="jstring">The Json config string</param>
    /// <returns>A MfdPanelConfig obj</returns>
    public static MfdPanelConfig FromJsonString( string jstring )
    {
      var c = vjMapping.FromJsonString<ConfigFile>( jstring );
      if ( c == default( ConfigFile ) ) {
        ErrorMsg = vjMapping.ErrorMsg;
      }
      return new MfdPanelConfig( c );
    }


    #region Class


    public bool Valid { get; } = false;
    private VJCommandDict m_commands = new VJCommandDict( );
    private Dictionary<string, string> m_labels = new Dictionary<string, string>( );
    private MacroDefList m_macros = null;

    /// <summary>
    /// A list of all labels from config
    /// </summary>
    public Dictionary<string, string> Labels { get => m_labels; }

    /// <summary>
    /// All commands as dictonary (string key = input name)
    /// </summary>
    public VJCommandDict VJCommands { get => m_commands; }

    /// <summary>
    /// All macros as MacroDefList
    /// </summary>
    public MacroDefList VJMacros { get => m_macros; }

    /// <summary>
    /// The name of the configuration
    /// </summary>
    public string ConfigName { get; } = "";

    /// <summary>
    /// The filename of the configuration background image
    /// </summary>
    public string BackgroundImageFile { get; } = "";

    /// <summary>
    /// cTor: decompose the Config file
    /// </summary>
    /// <param name="configFile"></param>
    public MfdPanelConfig( ConfigFile configFile )
    {
      if ( configFile == null ) return; // No configFile given Valid=> false;

      ConfigName = configFile.MapName;
      BackgroundImageFile = configFile.BackgroundImage;

      m_commands = configFile.VJCommandDict;
      m_macros = configFile.VJMacros;
      m_labels = configFile.Labels;

      Valid = m_commands.Count > 0;
    }

    /// <summary>
    /// Returns a VJCommand for the Input string or null
    /// </summary>
    /// <param name="input">The Switch Name</param>
    /// <returns>A VJCommand for the input or null if not found</returns>
    public VJCommand GetCommand( string input )
    {
      if ( m_commands.ContainsKey( input ) ) {
        return m_commands[input];
      }
      return null;
    }

    /// <summary>
    /// Returns the key name of the nth command
    /// </summary>
    /// <param name="buttonNo">Button No 1..28</param>
    /// <returns>The Input key for a button with number</returns>
    public string ButtonKey( int buttonNo )
    {
      buttonNo--; // we store them zero based internally

      if ( buttonNo < 0 ) return "";
      if ( buttonNo >= Labels.Count ) return "";

      return Labels.ElementAt( buttonNo ).Key;
    }

    /// <summary>
    /// Return the label for a button
    /// </summary>
    /// <param name="buttonNo">Button No 1..28</param>
    /// <returns>A string</returns>
    public string ButtonLabel( int buttonNo )
    {
      buttonNo--; // we store them zero based internally

      if ( buttonNo < 0 ) return $"N.A.{buttonNo}";
      if ( buttonNo >= m_labels.Count ) return $"N.A.{buttonNo}";

      return m_labels.ElementAt(buttonNo).Value;
    }

    #endregion


  }
}
