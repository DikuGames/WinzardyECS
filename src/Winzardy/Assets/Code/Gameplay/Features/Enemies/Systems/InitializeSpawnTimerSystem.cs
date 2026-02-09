using Code.Common.Entity;
using Code.Gameplay.Features.Enemies.Configs;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class InitializeSpawnTimerSystem : IInitializeSystem
    {
        private readonly EnemySpawnerConfig _spawnerConfig;

        public InitializeSpawnTimerSystem(IStaticDataService staticDataService)
        {
            _spawnerConfig = staticDataService.GetEnemySpawnerConfig();
        }

        public void Initialize()
        {
            CreateEntity.Empty()
                .AddSpawnTimer(_spawnerConfig.SpawnTimer);
        }
    }
}
