using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RsLib.Common;
using RsLib.LogMgr;
namespace RsLib.ToolDll
{
    public class TestPlugIn : IPlugIn
    {
        public string Name =>"Test Plug In";

        public void run(string msg)
        {
            string test = "123";

            Console.WriteLine($"test {msg}");
        }
    }
}
