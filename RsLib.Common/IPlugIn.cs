using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsLib.Common
{
    public interface IPlugIn
    {
        string Name { get; }

        void run(string msg);
    }
}
