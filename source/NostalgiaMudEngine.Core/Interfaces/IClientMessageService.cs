using NostalgiaMudEngine.Core.Models.InputOutput;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Interfaces
{
    public interface IClientMessageService
    {
        void Send(Output userOutput);
    }
}
