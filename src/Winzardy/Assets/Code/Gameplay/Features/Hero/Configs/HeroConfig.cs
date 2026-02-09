using UnityEngine;
using EntityBehaviour = Code.Infrastructure.View.EntityBehaviour;

namespace Code.Gameplay.Features.Hero.Configs
{
    [CreateAssetMenu(menuName = "Configs/Hero", fileName = "heroConfig")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public EntityBehaviour ViewPrefab { get; private set; }
        [field: SerializeField] public float MaxHp { get; private set; } = 100f;
        [field: SerializeField] public float Speed { get; private set; } = 2f;
        [field: SerializeField] public float PickupRadius { get; private set; } = 1f;
    }
}
