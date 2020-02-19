using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Models.EventArgs
{
    public class InputSubmittedEventArgs : System.EventArgs
    {
        public string UserId { get; set; }
        public string InputValue { get; set; }
    }
}
