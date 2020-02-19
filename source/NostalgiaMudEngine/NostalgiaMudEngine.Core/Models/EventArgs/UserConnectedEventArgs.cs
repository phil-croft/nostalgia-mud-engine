using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Models.EventArgs
{
    public class UserConnectedEventArgs : System.EventArgs
    {
        public string UserId { get; set; }
    }
}
