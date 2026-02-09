using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.System
{
    public class RotateAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public RotateAlongDirectionSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Direction,
                    GameMatcher.Transform,
                    GameMatcher.RotationAlignedAlongDirection));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                if (entity.Direction.sqrMagnitude < 0.01f)
                    continue;

                var forward = new Vector3(entity.Direction.x, 0f, entity.Direction.y);
                entity.Transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            }
        }
    }
}
