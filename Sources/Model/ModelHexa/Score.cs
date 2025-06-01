using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelHexa
{
    public class Score
    {
        public int Value { get; set; }


        public Score() { }
    

        public Score(int value)
        {
            Value = value;
        }
    }
}