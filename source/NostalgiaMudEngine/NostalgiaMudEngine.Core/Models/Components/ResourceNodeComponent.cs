using NostalgiaMudEngine.Core.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Models.Components
{
    public class ResourceNodeComponent : AbstractBaseComponent
    {
        public List<ResourceYield> ResourceYields { get; set; } = new List<ResourceYield>();
    }
}
