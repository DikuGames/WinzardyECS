using Code.Gameplay.Common.Time;
using Code.Gameplay.Common.Collisions;
using Code.Common.Extensions;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.System
{
    public class DirectionalDeltaMoveSystem : IExecuteSystem
    {
        private const float CharacterBlockingRadius = 0.45f;
        private const int OverlapBufferSize = 32;
        private static readonly Collider[] OverlapBuffer = new Collider[OverlapBufferSize];

        private readonly ITimeService _time;
        private readonly ICollisionRegistry _collisionRegistry;
        private readonly int _blockingLayerMask;
        private readonly IGroup<GameEntity> _movers;

        public DirectionalDeltaMoveSystem(GameContext game, ITimeService time, ICollisionRegistry collisionRegistry)
        {
            _time = time;
            _collisionRegistry = collisionRegistry;
            _blockingLayerMask = CollisionLayer.Hero.AsMask() | CollisionLayer.Enemy.AsMask();
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Speed,
                    GameMatcher.Direction,
                    GameMatcher.MovementAvailable,
                    GameMatcher.Moving));
        }
        
        public void Execute()
        {
            foreach (var mover in _movers)
            {
                Vector3 delta = new Vector3(mover.Direction.x, 0f, mover.Direction.y) * mover.Speed * _time.DeltaTime;
                Vector3 nextPosition = mover.WorldPosition + delta;

                if ((mover.isEnemy || mover.isHero) && IsBlockedByCharacter(mover, nextPosition))
                    continue;

                mover.ReplaceWorldPosition(nextPosition);
            }
        }

        private bool IsBlockedByCharacter(GameEntity mover, Vector3 nextPosition)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(
                nextPosition,
                CharacterBlockingRadius,
                OverlapBuffer,
                _blockingLayerMask,
                QueryTriggerInteraction.Ignore);

            for (int i = 0; i < hitCount; i++)
            {
                Collider hit = OverlapBuffer[i];
                if (hit == null)
                    continue;

                GameEntity hitEntity = _collisionRegistry.Get<GameEntity>(hit.GetInstanceID());
                if (hitEntity == null || hitEntity == mover)
                    continue;

                if (hitEntity.isHero || hitEntity.isEnemy)
                {
                    float currentSqrDistance = (hitEntity.WorldPosition - mover.WorldPosition).sqrMagnitude;
                    float nextSqrDistance = (hitEntity.WorldPosition - nextPosition).sqrMagnitude;

                    if (nextSqrDistance < currentSqrDistance)
                        return true;
                }
            }

            return false;
        }
    }
}
