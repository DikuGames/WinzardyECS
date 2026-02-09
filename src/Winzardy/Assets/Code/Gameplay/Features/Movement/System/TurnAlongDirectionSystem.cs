using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.System
{
    public class TurnAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public TurnAlongDirectionSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TurnedAlongDirection,
                    GameMatcher.Direction,
                    GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (var mover in _movers)
            {
                if (mover.Direction.sqrMagnitude < 0.01f)
                    continue;

                var right = new Vector3(mover.Direction.x, 0f, mover.Direction.y);
                mover.Transform.rotation = Quaternion.LookRotation(Vector3.Cross(right, Vector3.up), Vector3.up);
            }
        }
    }
}
