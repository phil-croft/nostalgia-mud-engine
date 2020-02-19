using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Models.Components
{
    public abstract class AbstractBaseComponent
    {
        public Guid EntityId { get; set; }
        public Entity Entity { get; set; }
        public bool IsActive { get; set; }
    }
}
