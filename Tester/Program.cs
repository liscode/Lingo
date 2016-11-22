using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["LingoCS"].ConnectionString;
            BL.BL bl = new BL.BL(cs);
            string randomWord= bl.GetRandomWord();


        }
    }
}
