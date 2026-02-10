using Code.Gameplay.Features.Enemies.Behaviours;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Registrar
{
    public class EnemyHealthRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private EnemyHealthView _healthView;
        
        public override void RegisterComponents()
        {
            Entity
                .AddEnemyHealth(_healthView);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasEnemyHealth)
                Entity.RemoveEnemyHealth();
        }
    }
}