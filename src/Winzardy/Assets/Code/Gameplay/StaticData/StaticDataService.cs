using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Enemies.Configs;
using Code.Gameplay.Features.Hero.Configs;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<AbilityId,AbilityConfig> _abilityById;
    private Dictionary<EnemyTypeId, EnemyConfig> _enemyById;
    private EnemySpawnerConfig _enemySpawnerConfig;
    private HeroConfig _heroConfig;
    private Dictionary<LootTypeId, LootConfig> _lootById;
    private Dictionary<WindowId, GameObject> _windowPrefabsById;

    public void LoadAll()
    {
      LoadAbilities();
      LoadEnemies();
      LoadHero();
      LoadLoot();
      LoadWindows();
    }

    public AbilityConfig GetAbilityConfig(AbilityId abilityId)
    {
      if (_abilityById.TryGetValue(abilityId, out AbilityConfig config))
        return config;

      throw new Exception($"Ability config for {abilityId} was not found");
    }

    public EnemyConfig GetEnemyConfig(EnemyTypeId enemyTypeId)
    {
      if (_enemyById.TryGetValue(enemyTypeId, out EnemyConfig config))
        return config;

      throw new Exception($"Enemy config for {enemyTypeId} was not found");
    }

    public EnemySpawnerConfig GetEnemySpawnerConfig() =>
      _enemySpawnerConfig != null
        ? _enemySpawnerConfig
        : throw new Exception("Enemy spawner config was not found");

    public HeroConfig GetHeroConfig() =>
      _heroConfig != null
        ? _heroConfig
        : throw new Exception("Hero config was not found");

    public LootConfig GetLootConfig(LootTypeId lootTypeId)
    {
      if (_lootById.TryGetValue(lootTypeId, out LootConfig config))
        return config;

      throw new Exception($"Loot config for {lootTypeId} was not found");
    }

    public AbilityData GetAbility(AbilityId abilityId)
    {
      AbilityConfig config = GetAbilityConfig(abilityId);

      return config.AbilityData;
    }

    public GameObject GetWindowPrefab(WindowId id) =>
      _windowPrefabsById.TryGetValue(id, out GameObject prefab)
        ? prefab
        : throw new Exception($"Prefab config for window {id} was not found");

    private void LoadAbilities()
    {
      _abilityById = Resources
        .LoadAll<AbilityConfig>("Configs/Abilities")
        .ToDictionary(x => x.AbilityId, x => x);
    }

    private void LoadLoot()
    {
      _lootById = Resources
        .LoadAll<LootConfig>("Configs/Loots")
        .ToDictionary(x => x.LootTypeId, x => x);
    }

    private void LoadHero()
    {
      _heroConfig = Resources.Load<HeroConfig>("Configs/Heroes/heroConfig");
    }

    private void LoadEnemies()
    {
      _enemyById = Resources
        .LoadAll<EnemyConfig>("Configs/Enemies")
        .ToDictionary(x => x.EnemyTypeId, x => x);

      _enemySpawnerConfig = Resources.Load<EnemySpawnerConfig>("Configs/Enemies/EnemySpawnerConfig");
    }

    private void LoadWindows()
    {
      _windowPrefabsById = Resources
        .Load<WindowsConfig>("Configs/Windows/windowsConfig")
        .WindowConfigs
        .ToDictionary(x => x.Id, x => x.Prefab);
    }
  }
}
