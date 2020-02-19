using Microsoft.Extensions.Logging;
using NostalgiaMudEngine.Core.Constants;
using NostalgiaMudEngine.Core.Extensions;
using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Core.Models;
using NostalgiaMudEngine.Core.Models.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Services
{
    internal class EntityManager : IEntityManager
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<EntityManager> _logger;
        private const int ThresholdAdjustmentAmountForUndefinedEntities = 1000;
        private const int LowerThresholdOfUndefinedEntities = 100000;
        private const int UpperThresholdOfUndefinedEntities = 500000;

        private readonly Queue<Entity> _unloadedEntities = new Queue<Entity>();
        private readonly HashSet<Entity> _loadedEntities = new HashSet<Entity>();
        private readonly Dictionary<Guid, Entity> _loadedEntitiesById = new Dictionary<Guid, Entity>();
        private readonly Dictionary<string, HashSet<object>> _loadedComponentsByName = new Dictionary<string, HashSet<object>>();
        private readonly Dictionary<string, Entity> _blueprintEntities = new Dictionary<string, Entity>();

        private bool _isLoaded = false;

        public EntityManager(
            IEventDispatcher eventDispatcher,
            ILogger<EntityManager> logger
            )
        {
            _eventDispatcher = eventDispatcher.ThrowIfArgumentNull<IEventDispatcher>(nameof(eventDispatcher));
            _logger = logger.ThrowIfArgumentNull<ILogger<EntityManager>>(nameof(logger));

            _eventDispatcher.ShutdownRequested += OnShutdownRequested;
        }

        #region Public Methods
        public Entity CreateInstanceOf(string blueprintName)
        {
            if (!_isLoaded)
                LoadFromStorage();

            UpdateAllocationOfUndefinedEntitiesIfNecessary();
            var entity = _unloadedEntities.Dequeue();
            entity = ReplaceWithBlueprintValues(entity, blueprintName);
            _loadedEntities.Add(entity);
            UpdateComponentTracking(entity, true);

            return entity;
        }

        public void Destroy(Entity entity)
        {
            _loadedEntities.Remove(entity);
            _loadedEntitiesById.Remove(entity.Id);
            _unloadedEntities.Enqueue(entity);
            UpdateComponentTracking(entity, false);
            UpdateAllocationOfUndefinedEntitiesIfNecessary();
        }

        public Entity GetByEntityId(Guid id)
        {
            Entity entity = null;
            _loadedEntitiesById.TryGetValue(id, out entity);
            return entity;
        }

        public List<Entity> GetManyByComponentPredicate<T>(Func<T, bool> predicate) where T : AbstractBaseComponent
        {
            List<Entity> matchingEntities = new List<Entity>();
            var components = _loadedComponentsByName[typeof(T).Name];

            foreach (T component in components)
            {
                if (predicate(component))
                    matchingEntities.Add(component.Entity);
            }

            return matchingEntities;
        }

        public List<Entity> GetManyByPredicate(Func<Entity, bool> predicate)
        {
            List<Entity> matchingEntities = new List<Entity>();

            foreach (Entity entity in _loadedEntities)
            {
                if (predicate(entity))
                    matchingEntities.Add(entity);
            }

            return matchingEntities;
        }
        #endregion

        #region Private Methods
        private Entity BuildEntity()
        {
            var entity = new Entity()
            {
                Id = Guid.NewGuid()
            };

            entity.CharacterComponent = new CharacterComponent()
            {
                Entity = entity,
                EntityId = entity.Id
            };

            entity.MotionComponent = new MotionComponent()
            {
                Entity = entity,
                EntityId = entity.Id
            };

            entity.PlayerComponent = new PlayerComponent()
            {
                Entity = entity,
                EntityId = entity.Id
            };

            entity.PositionComponent = new PositionComponent()
            {
                Entity = entity,
                EntityId = entity.Id
            };

            entity.ResourceNodeComponent = new ResourceNodeComponent()
            {
                Entity = entity,
                EntityId = entity.Id
            };

            entity.WeaponComponent = new WeaponComponent()
            {
                Entity = entity,
                EntityId = entity.Id
            };

            return entity;
        }

        private void OnShutdownRequested(object sender, EventArgs args)
        {
            _loadedEntities.Clear();
            _loadedEntitiesById.Clear();
            _unloadedEntities.Clear();
        }

        private void LoadFromStorage()
        {
            //var entities = _entityRepository.GetAllEntities();

            //foreach (GameEntity entity in entities)
            //{
            //    if (!String.IsNullOrWhiteSpace(entity.BlueprintName))
            //        _blueprintEntities.Add(entity.BlueprintName, entity);
            //    else
            //    {
            //        _loadedEntities.Add(entity);
            //        _loadedEntitiesById.Add(entity.Id, entity);
            //    }
            //}

            // Temporary -- making blueprints in memory.

            // Terrain BP
            var terrainEntityBp = BuildEntity();
            terrainEntityBp.BlueprintHistory = BlueprintNames.Terrain;
            terrainEntityBp.PositionComponent.IsActive = true;
            terrainEntityBp.IsBlueprint = true;
            _blueprintEntities.Add(terrainEntityBp.BlueprintHistory, terrainEntityBp);



            _logger.LogInformation($"{nameof(EntityManager)} loaded {_loadedEntities.Count} entities and {_blueprintEntities.Count} blueprints.");

            _isLoaded = true;
        }

        private Entity ReplaceWithBlueprintValues(Entity entity, string blueprintName)
        {
            entity.ThrowIfArgumentNull<Entity>(nameof(entity));

            if (!_blueprintEntities.TryGetValue(blueprintName, out Entity blueprintEntity))
                throw new Exception($"Unable to find blueprint by name: {blueprintName}.");

            if (blueprintEntity != null)
            {
                entity.BlueprintHistory = blueprintEntity.BlueprintHistory;
                entity.IsBlueprint = false;

                entity.CharacterComponent.IsActive = blueprintEntity.CharacterComponent.IsActive;

                entity.MotionComponent.IsActive = blueprintEntity.MotionComponent.IsActive;

                entity.PlayerComponent.IsActive = blueprintEntity.PlayerComponent.IsActive;
                entity.PlayerComponent.UserId = blueprintEntity.PlayerComponent.UserId;

                entity.PositionComponent.IsActive = blueprintEntity.PositionComponent.IsActive;
                entity.PositionComponent.X = blueprintEntity.PositionComponent.X;
                entity.PositionComponent.Y = blueprintEntity.PositionComponent.Y;
                entity.PositionComponent.Z = blueprintEntity.PositionComponent.Z;

                entity.ResourceNodeComponent.IsActive = blueprintEntity.ResourceNodeComponent.IsActive;
                entity.ResourceNodeComponent.ResourceYields = blueprintEntity.ResourceNodeComponent.ResourceYields;

                entity.WeaponComponent.IsActive = blueprintEntity.WeaponComponent.IsActive;
            }

            return entity;
        }

        private void UpdateAllocationOfUndefinedEntitiesIfNecessary()
        {
            var undefinedEntityCount = _unloadedEntities.Count;

            bool isAboveThreshold = undefinedEntityCount > UpperThresholdOfUndefinedEntities;
            bool isBelowThreshold = undefinedEntityCount < LowerThresholdOfUndefinedEntities;

            if (isAboveThreshold || isBelowThreshold)
            {
                for (var count = 1; count <= ThresholdAdjustmentAmountForUndefinedEntities; count++)
                {
                    if (isAboveThreshold)
                        _unloadedEntities.Dequeue();
                    else
                    {
                        var entity = BuildEntity();
                        _unloadedEntities.Enqueue(entity);
                    }
                }
            }
        }

        private void UpdateComponentTracking(Entity entity, bool track)
        {
            Action<object> updateComponent = (component) =>
            {
                var componentName = component.GetType().Name;
                if (!_loadedComponentsByName.TryGetValue(componentName, out HashSet<object> components))
                {
                    components = new HashSet<object>();
                    _loadedComponentsByName.Add(componentName, components);
                }

                if (track && !components.Contains(component))
                    _loadedComponentsByName[componentName].Add(component);
                else if (!track && components.Contains(componentName))
                    _loadedComponentsByName[componentName].Remove(component);
            };

            updateComponent(entity.CharacterComponent);
            updateComponent(entity.MotionComponent);
            updateComponent(entity.PlayerComponent);
            updateComponent(entity.PositionComponent);
            updateComponent(entity.ResourceNodeComponent);
            updateComponent(entity.WeaponComponent);
        }
        #endregion
    }
}
