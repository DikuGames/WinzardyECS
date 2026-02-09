using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Factory;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyDropLootSystem : IExecuteSystem
    {
        private readonly ILootFactory _lootFactory;
        private readonly IStaticDataService _staticData;
        private readonly IGroup<GameEntity> _enemies;

        public EnemyDropLootSystem(GameContext game, ILootFactory lootFactory, IStaticDataService staticData)
        {
            _lootFactory = lootFactory;
            _staticData = staticData;
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.EnemyTypeId,
                    GameMatcher.WorldPosition,
                    GameMatcher.Dead,
                    GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (GameEntity enemy in _enemies)
            {
                float coinDropChance = _staticData.GetEnemyConfig(enemy.EnemyTypeId).CoinDropChance;
                if (Random.Range(0f, 1f) <= coinDropChance)
                    _lootFactory.CreateLootItem(LootTypeId.Coin, enemy.WorldPosition);
            }
        }
    }
}
