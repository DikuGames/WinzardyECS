using UnityEngine;
using EntityBehaviour = Code.Infrastructure.View.EntityBehaviour;

namespace Code.Gameplay.Features.Enemies.Configs
{
    [CreateAssetMenu(menuName = "Configs/Enemy", fileName = "enemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }
        [field: SerializeField] public EntityBehaviour ViewPrefab { get; private set; }
        [field: SerializeField] public float MaxHp { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; }
    }
}
