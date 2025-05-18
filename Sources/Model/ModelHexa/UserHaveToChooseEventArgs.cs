using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class UserHaveToChooseEventArgs: EventArgs
    {
        public string Question {  get; set; }
        public UserHaveToChooseEventArgs(string question)
        {
            Question = question;
        }
    }
}
