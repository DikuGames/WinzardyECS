using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CreateEffectsOnReachedTargetSystem : IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _producers;
        private readonly List<GameEntity> _buffer = new(64);

        public CreateEffectsOnReachedTargetSystem(GameContext game, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _producers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Reached,
                    GameMatcher.EffectSetups,
                    GameMatcher.TargetsBuffer));
        }

        public void Execute()
        {
            foreach (GameEntity producer in _producers.GetEntities(_buffer))
            {
                foreach (int targetId in producer.TargetsBuffer)
                foreach (EffectSetup setup in producer.EffectSetups)
                    _effectFactory.CreateEffect(setup, producer.Id, targetId);

                producer.isReached = false;
            }
        }
    }
}
