using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFDlib.OwnerSupport
{
  /// <summary>
  /// Supporting methods for MFD handling
  /// </summary>
  public class Support
  {
    /// <summary>
    /// A class to maintain any number of MFDs
    /// </summary>
    public class DictMFD
    {
      private Dictionary<string, IUC_MFD> m_mfds = new Dictionary<string, IUC_MFD>( );

      public IEnumerable<IUC_MFD> UserControls { get => m_mfds.Values.AsEnumerable( ); }

      public void Add( string key, IUC_MFD control )
      {
        if ( m_mfds.ContainsKey( key ) ) return; // nope..

        m_mfds.Add( key, control );
      }

      public IUC_MFD MFD( string key )
      {
        if ( m_mfds.ContainsKey( key ) ) return null; // nope..

        return m_mfds[key];
      }

      public int Count { get => m_mfds.Count; }

    }

    /// <summary>
    /// Convert from a Ressourcefile to it's string
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string FromResourceFile( byte[] bytes )
    {
      return Encoding.Default.GetString( bytes );
    }

    /// <summary>
    /// Tries to find a file either with the given path
    /// or relative in ConfigFiles folder
    /// </summary>
    /// <param name="filename">A filename</param>
    /// <returns>A valid filename or an empty string</returns>
    public static string FileLocator( string filename )
    {
      string fName = filename;

      if ( Path.IsPathRooted( filename ) ) {
        if ( File.Exists( fName ) ) {
          return fName;
        }
        else {
          return "";
        }
      }
      // not rooted
      if ( !File.Exists( fName ) ) {
        fName = Path.Combine( "ConfigFiles", filename ); // try a relative one
      }
      if ( File.Exists( fName ) ) {
        return fName;
      }
      return "";
    }


    /// <summary>
    /// Returns the file content as string
    ///   tries the absolute path and then in "ConfigFiles"
    /// </summary>
    /// <param name="textFilename">Fully qualified textfile name</param>
    /// <returns>File content as string or an empty string</returns>
    public static string FileAsString( string textFilename )
    {
      string fName = FileLocator( textFilename );
      try {
        return File.ReadAllText( fName, Encoding.Default );
      }
#pragma warning disable CS0168 // Variable is declared but never used
      catch ( Exception e ) {
#pragma warning restore CS0168 // Variable is declared but never used
        return "";
      }
    }


    /// <summary>
    /// A test config string 
    /// </summary>
    public const string TestConfig1 =
      @"{
          ""_Comment"" : ""Cougar MFD Panel Test Config File 1"",
          ""BackgroundImage"":  ""invalid"",
          ""MapName"" : ""TestConfig1"",
          ""SwitchMap"": [
              { ""Input"": ""BT1"",  ""Cmd"" : [{ ""B"": {""Index"": 1 }}]  },
              { ""Input"": ""BT2"",  ""Cmd"" : [{ ""B"": {""Index"": 2 }}]  },
              { ""Input"": ""BT3"",  ""Cmd"" : [{ ""B"": {""Index"": 3 }}]  },
              { ""Input"": ""BT4"",  ""Cmd"" : [{ ""B"": {""Index"": 4 }}]  },
              { ""Input"": ""BT5"",  ""Cmd"" : [{ ""B"": {""Index"": 5 }}]  },
              { ""Input"": ""BT6"",  ""Cmd"" : [{ ""B"": {""Index"": 6 }}]  },
              { ""Input"": ""BT7"",  ""Cmd"" : [{ ""B"": {""Index"": 7 }}]  },
              { ""Input"": ""BT8"",  ""Cmd"" : [{ ""B"": {""Index"": 8 }}]  },
              { ""Input"": ""BT9"",  ""Cmd"" : [{ ""B"": {""Index"": 9 }}]  },
              { ""Input"": ""BT10"", ""Cmd"" : [{ ""B"": {""Index"": 10 }}]  },
              { ""Input"": ""BT11"", ""Cmd"" : [{ ""B"": {""Index"": 11 }}]  },
              { ""Input"": ""BT12"", ""Cmd"" : [{ ""B"": {""Index"": 12 }}]  },
              { ""Input"": ""BT13"", ""Cmd"" : [{ ""B"": {""Index"": 13 }}]  },
              { ""Input"": ""BT14"", ""Cmd"" : [{ ""B"": {""Index"": 14 }}]  },
              { ""Input"": ""BT15"", ""Cmd"" : [{ ""B"": {""Index"": 15 }}]  },
              { ""Input"": ""BT16"", ""Cmd"" : [{ ""B"": {""Index"": 16 }}]  },
              { ""Input"": ""BT17"", ""Cmd"" : [{ ""B"": {""Index"": 17 }}]  },
              { ""Input"": ""BT18"", ""Cmd"" : [{ ""B"": {""Index"": 18 }}]  },
              { ""Input"": ""BT19"", ""Cmd"" : [{ ""B"": {""Index"": 19 }}]  },
              { ""Input"": ""BT20"", ""Cmd"" : [{ ""B"": {""Index"": 20 }}]  },
              { ""Input"": ""BT21"", ""Cmd"" : [{ ""B"": {""Index"": 21, ""Ext3"": ""NextMFD"" }}]  },
              { ""Input"": ""BT22"", ""Cmd"" : [{ ""B"": {""Index"": 22, ""Ext3"": ""PrevMFD"" }}]  },
              { ""Input"": ""BT23"", ""Cmd"" : [{ ""B"": {""Index"": 23 }}]  },
              { ""Input"": ""BT24"", ""Cmd"" : [{ ""B"": {""Index"": 24 }}]  },
              { ""Input"": ""BT25"", ""Cmd"" : [{ ""B"": {""Index"": 25 }}]  },
              { ""Input"": ""BT26"", ""Cmd"" : [{ ""B"": {""Index"": 26 }}]  },
              { ""Input"": ""BT27"", ""Cmd"" : [{ ""B"": {""Index"": 27 }}]  },
              { ""Input"": ""BT28"", ""Cmd"" : [{ ""B"": {""Index"": 28 }}]  }              
            ],
          ""ButtonLabels"": [
              ""BTN01"", ""BTN02"", ""BTN03"", ""BTN04"", ""BTN05"",  ""BTN06"", ""BTN07"", ""BTN08"", ""BTN09"", ""BTN10"",
              ""BTN11"", ""BTN12"", ""BTN13"", ""BTN14"", ""BTN15"",  ""BTN16"", ""BTN17"", ""BTN18"", ""BTN19"", ""BTN20"",
              ""MFD SW"", """",   ""SW 23"", """",   ""SW 25"", """",   ""SW 27"", """"
            ]
        }";


    /// <summary>
    /// A test config string 
    /// </summary>
    public const string TestConfig2 =
      "{" +
       "   \"_Comment\" : \"Cougar MFD Panel Test Config File 2\"," +
       "   \"MapName\" : \"TestConfig2\"," +
       "   \"BackgroundImage\":  \"invalid\"," +
       "  \"SwitchMap\": [" +
       "       { \"Input\": \"BT1\",  \"Cmd\" : [{ \"B\": {\"Index\": 1 }}]  }," +
       "       { \"Input\": \"BT2\",  \"Cmd\" : [{ \"B\": {\"Index\": 2 }}]  }," +
       "       { \"Input\": \"BT3\",  \"Cmd\" : [{ \"B\": {\"Index\": 3 }}]  }," +
       "       { \"Input\": \"BT4\",  \"Cmd\" : [{ \"B\": {\"Index\": 4 }}]  }," +
       "       { \"Input\": \"BT5\",  \"Cmd\" : [{ \"B\": {\"Index\": 5 }}]  }," +
       "       { \"Input\": \"BT6\",  \"Cmd\" : [{ \"B\": {\"Index\": 6 }}]  }," +
       "       { \"Input\": \"BT7\",  \"Cmd\" : [{ \"B\": {\"Index\": 7 }}]  }," +
       "       { \"Input\": \"BT8\",  \"Cmd\" : [{ \"B\": {\"Index\": 8 }}]  }," +
       "       { \"Input\": \"BT9\",  \"Cmd\" : [{ \"B\": {\"Index\": 9 }}]  }," +
       "       { \"Input\": \"BT10\", \"Cmd\" : [{ \"B\": {\"Index\": 10 }}]  }," +
       "       { \"Input\": \"BT11\", \"Cmd\" : [{ \"B\": {\"Index\": 11 }}]  }," +
       "       { \"Input\": \"BT12\", \"Cmd\" : [{ \"B\": {\"Index\": 12 }}]  }," +
       "       { \"Input\": \"BT13\", \"Cmd\" : [{ \"B\": {\"Index\": 13 }}]  }," +
       "       { \"Input\": \"BT14\", \"Cmd\" : [{ \"B\": {\"Index\": 14 }}]  }," +
       "       { \"Input\": \"BT15\", \"Cmd\" : [{ \"B\": {\"Index\": 15 }}]  }," +
       "       { \"Input\": \"BT16\", \"Cmd\" : [{ \"B\": {\"Index\": 16 }}]  }," +
       "       { \"Input\": \"BT17\", \"Cmd\" : [{ \"B\": {\"Index\": 17 }}]  }," +
       "       { \"Input\": \"BT18\", \"Cmd\" : [{ \"B\": {\"Index\": 18 }}]  }," +
       "       { \"Input\": \"BT19\", \"Cmd\" : [{ \"B\": {\"Index\": 19 }}]  }," +
       "       { \"Input\": \"BT20\", \"Cmd\" : [{ \"B\": {\"Index\": 20 }}]  }," +
       "       { \"Input\": \"BT21\", \"Cmd\" : [{ \"B\": {\"Index\": 21, \"Ext3\": \"NextMFD\" }}]  }," + // panel switcher
       "       { \"Input\": \"BT22\", \"Cmd\" : [{ \"B\": {\"Index\": 22, \"Ext3\": \"PrevMFD\" }}]  }," + // panel switcher
       "       { \"Input\": \"BT23\", \"Cmd\" : [{ \"B\": {\"Index\": 23 }}]  }," +
       "       { \"Input\": \"BT24\", \"Cmd\" : [{ \"B\": {\"Index\": 24 }}]  }," +
       "       { \"Input\": \"BT25\", \"Cmd\" : [{ \"B\": {\"Index\": 25 }}]  }," +
       "       { \"Input\": \"BT26\", \"Cmd\" : [{ \"B\": {\"Index\": 26 }}]  }," +
       "       { \"Input\": \"BT27\", \"Cmd\" : [{ \"B\": {\"Index\": 27 }}]  }," +
       "       { \"Input\": \"BT28\", \"Cmd\" : [{ \"B\": {\"Index\": 28 }}]  } " +
       "     ]," +
       "   \"ButtonLabels\": [" +
       "       \"LOCK¬DOOR\", \"OPEN¬DOOR\", \"BTN03\", \"BTN04\", \"BTN05\",  \"BTN06\", \"BTN07\", \"BTN08\", \"BTN09\", \"BTN10\"," +
       "       \"BTN11\", \"BTN12\", \"BTN13\", \"BTN14\", \"BTN15\",  \"BTN16\", \"BTN17\", \"BTN18\", \"BTN19\", \"BTN20\"," +
       "       \"MFD SW\", \"\",   \"SW 23\", \"\",   \"SW 25\", \"\",   \"SW 27\", \"\"" +
       "     ]" +
       " }";
  }


}

