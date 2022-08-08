using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCPBase
{
    public enum ConState
    {
        None,
        listen,
        Opening,
        Opened,
        Connected
    }
}
