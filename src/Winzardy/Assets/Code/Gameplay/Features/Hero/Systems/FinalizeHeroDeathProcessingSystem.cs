using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class FinalizeHeroDeathProcessingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private List<GameEntity> _buffer = new(128);

        public FinalizeHeroDeathProcessingSystem(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.ProcessingDeath,
                    GameMatcher.Dead));
        }
        
        public void Execute()
        {
            foreach (var hero in _heroes.GetEntities(_buffer))
            {
                hero.isProcessingDeath = false;
            }
        }
    }
}