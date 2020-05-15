using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib
{
  public class UC_MFD_CmdEventArgs
  {
    /// <summary>
    /// MFD Panel Events
    /// </summary>
    public UC_MFD_CmdEventArgs( MfdInstance mfd, string commandArgument, bool pressed )
    {
      Mfd = mfd;
      CommandArgument = commandArgument;
      Pressed = pressed;
    }

    public MfdInstance Mfd { get; private set; }
    public string CommandArgument { get; private set; }
    public bool Pressed { get; private set; }
  }

}

