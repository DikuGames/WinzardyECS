using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Effects.Systems
{
    public class ProcessDamageEffectSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;

        public ProcessDamageEffectSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.DamageEffect,
                    GameMatcher.EffectValue,
                    GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (GameEntity effect in _effects)
            {
                GameEntity target = effect.Target();

                effect.isProcessed = true;
                
                if (target.isDead)
                    continue;

                var hp = Mathf.Clamp(target.CurrentHp - effect.EffectValue, 0, target.MaxHp);
                target.ReplaceCurrentHp(hp);
                
                if (target.hasEnemyHealth)
                    target.EnemyHealth.SetHp((int)target.CurrentHp, (int)target.MaxHp);
            }
        }
    }
}