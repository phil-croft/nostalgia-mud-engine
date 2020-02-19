using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Models.InputOutput
{
    public class Input
    {
        public string UserId { get; set; }
        public Guid? NpcEntityId { get; set; }
        public string Value { get; set; }
    }
}
