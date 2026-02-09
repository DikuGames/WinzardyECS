using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Configs
{
  [CreateAssetMenu(menuName = "Configs/Ability", fileName = "abilityConfig")]
  public class AbilityConfig : ScriptableObject
  {
    [field: SerializeField] public AbilityId AbilityId { get; private set; }
    [field: SerializeField] public AbilityData AbilityData { get; private set; }
  }
}
