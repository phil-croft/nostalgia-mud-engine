using NostalgiaMudEngine.Core.Models.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

        public string BlueprintHistory { get; set; }
        public CharacterComponent CharacterComponent { get; set; }
        public MotionComponent MotionComponent { get; set; }
        public PlayerComponent PlayerComponent { get; set; }
        public PositionComponent PositionComponent { get; set; }
        public ResourceNodeComponent ResourceNodeComponent { get; set; }
        public WeaponComponent WeaponComponent { get; set; }
        public bool IsBlueprint { get; set; }
    }
}
