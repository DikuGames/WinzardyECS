using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class ProjectileAbilitySystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;
        private readonly List<GameEntity> _buffer = new(1);
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;

        public ProjectileAbilitySystem(
            GameContext game, 
            IStaticDataService staticDataService, 
            IArmamentFactory armamentFactory)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;

            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ProjectileAbility,
                    GameMatcher.CooldownUp));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
            
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition));
        }
        
        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            foreach (GameEntity hero in _heroes)
            {
                if (_enemies.count <= 0)
                    continue;

                GameEntity target = ClosestTarget(hero);
                if (target == null)
                    continue;

                _armamentFactory
                    .CreateProjectile(hero.WorldPosition)
                    .AddProducerId(hero.Id)
                    .ReplaceDirection(DirectionToTargetXZ(hero, target))
                    .With(x => x.isMoving = true);
                
                ability.PutOnCooldown(_staticDataService.GetAbility(AbilityId.Projectile).Cooldown);
            }
        }

        private static UnityEngine.Vector2 DirectionToTargetXZ(GameEntity from, GameEntity target)
        {
            UnityEngine.Vector3 delta = target.WorldPosition - from.WorldPosition;
            UnityEngine.Vector2 direction = new UnityEngine.Vector2(delta.x, delta.z);
            return direction.sqrMagnitude > 0f ? direction.normalized : UnityEngine.Vector2.zero;
        }

        private GameEntity ClosestTarget(GameEntity hero)
        {
            GameEntity closest = null;
            float closestSqr = float.MaxValue;

            foreach (GameEntity enemy in _enemies)
            {
                float sqr = (enemy.WorldPosition - hero.WorldPosition).sqrMagnitude;
                if (sqr < closestSqr)
                {
                    closestSqr = sqr;
                    closest = enemy;
                }
            }

            return closest;
        }
    }
}



