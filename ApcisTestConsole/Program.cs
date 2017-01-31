using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApcisTestConsole.Debuggers;

namespace ApcisTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PrestationsControllerDebug.Create();
            while (true) ;
        }
    }
}
