using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using UnityEngine;
using EntityBehaviour = Code.Infrastructure.View.EntityBehaviour;

namespace Code.Gameplay.Features.Loot.Configs
{
    [CreateAssetMenu(menuName = "Configs/LootConfig", fileName = "LootConfig")]
    public class LootConfig : ScriptableObject
    {
        [field: SerializeField] public LootTypeId LootTypeId { get; private set; }
        [field: SerializeField] public EntityBehaviour ViewPrefab { get; private set; }
        [field: SerializeField] public List<EffectSetup> EffectSetups { get; private set; }
    }
}
