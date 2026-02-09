using Entitas;

using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class ChaseHeroSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _heroes;

        public ChaseHeroSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition));
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }
        
        public void Execute()
        {
            foreach (var hero in _heroes)
            foreach (var enemy in _enemies)
            {
                Vector3 delta = hero.WorldPosition - enemy.WorldPosition;
                Vector2 direction = new Vector2(delta.x, delta.z);
                enemy.ReplaceDirection(direction.sqrMagnitude > 0f ? direction.normalized : Vector2.zero);
                enemy.isMoving = true;
            }
        }
    }
}
