using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Enemies.Configs;
using Code.Gameplay.Features.Hero.Configs;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.Windows;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public interface IStaticDataService
  {
    void LoadAll();
    AbilityData GetAbility(AbilityId abilityId);
    EnemyConfig GetEnemyConfig(EnemyTypeId enemyTypeId);
    EnemySpawnerConfig GetEnemySpawnerConfig();
    HeroConfig GetHeroConfig();
    LootConfig GetLootConfig(LootTypeId lootTypeId);
    
    GameObject GetWindowPrefab(WindowId id);
  }
}
