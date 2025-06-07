using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class WrongInputEventArgs:EventArgs
    {
        public string ErrorMessage {  get; set; }
        public List<string> ForbiddenWords { get; set; }
        public WrongInputEventArgs(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
