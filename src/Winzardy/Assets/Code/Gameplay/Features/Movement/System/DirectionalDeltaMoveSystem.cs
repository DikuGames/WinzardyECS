using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.System
{
    public class DirectionalDeltaMoveSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _movers;

        public DirectionalDeltaMoveSystem(GameContext game, ITimeService time)
        {
            _time = time;
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
                mover.ReplaceWorldPosition(mover.WorldPosition + delta);
            }
        }
    }
}
