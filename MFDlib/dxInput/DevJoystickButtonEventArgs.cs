using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.dxInput
{
  internal class DevJoystickButtonEventArgs : EventArgs
  {

    public DevJoystickButtonEventArgs( int buttonNo )
    {

      ButtonNo = buttonNo;
    }

    public int ButtonNo { get; private set; }
        
  }

}
