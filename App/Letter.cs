using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Letter
    {
        public char Value { get; set; }
        public ConsoleColor Background { get; set; }

        public Letter(char value, ConsoleColor background)
        {
            this.Value = value;
            this.Background = background;
        }
    }
}
