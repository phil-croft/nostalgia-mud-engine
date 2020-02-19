using NostalgiaMudEngine.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Models.Components
{
    public class MotionComponent : AbstractBaseComponent
    {
        public Guid? PreviousPositionEntityId { get; set; }
        public int Velocity { get; set; }
        public Directions? Trajectory { get; set; }
        public int MaxDistance { get; set; }
        public int CurrentDistance { get; set; }
        public MovementTypes MovementType { get; set; }
    }
}
