using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class SetHeroDirectionByInput : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<InputEntity> _inputs;

        public SetHeroDirectionByInput(GameContext game, InputContext input)
        {
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.MovementAvailable));
            _inputs = input.GetGroup(InputMatcher.Input);
        }
        
        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            foreach (GameEntity hero in _heroes)
            {
                hero.isMoving = input.hasAxisInput;

                if (input.hasAxisInput)
                    hero.ReplaceDirection(input.AxisInput.normalized);
            }
        }
    }
}