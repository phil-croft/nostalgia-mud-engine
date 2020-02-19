using NostalgiaMudEngine.Core.Models;
using NostalgiaMudEngine.Core.Models.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Interfaces
{
    internal interface IEntityManager
    {
        Entity CreateInstanceOf(string blueprintName);
        void Destroy(Entity entity);
        Entity GetByEntityId(Guid id);
        List<Entity> GetManyByComponentPredicate<T>(Func<T, bool> predicate) where T : AbstractBaseComponent;
        List<Entity> GetManyByPredicate(Func<Entity, bool> predicate);
    }
}
