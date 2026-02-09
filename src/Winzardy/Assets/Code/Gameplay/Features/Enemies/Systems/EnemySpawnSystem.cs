using Code.Common.Extensions;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Enemies.Configs;
using Code.Gameplay.Features.Enemies.Factory;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemySpawnSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ICameraProvider _cameraProvider;
        private readonly EnemySpawnerConfig _spawnerConfig;
        private readonly IGroup<GameEntity> _timers;
        private readonly IGroup<GameEntity> _heroes;

        public EnemySpawnSystem(
            GameContext game,
            ITimeService time,
            IEnemyFactory enemyFactory,
            ICameraProvider cameraProvider,
            IStaticDataService staticDataService)
        {
            _time = time;
            _enemyFactory = enemyFactory;
            _cameraProvider = cameraProvider;
            _spawnerConfig = staticDataService.GetEnemySpawnerConfig();

            _timers = game.GetGroup(GameMatcher.SpawnTimer);
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }
        
        public void Execute()
        {
            foreach (var hero in _heroes)
            foreach (var timer in _timers)
            {
                timer.ReplaceSpawnTimer(timer.SpawnTimer - _time.DeltaTime);

                if (timer.SpawnTimer <= 0)
                {
                    timer.ReplaceSpawnTimer(_spawnerConfig.SpawnTimer);
                    _enemyFactory.CreateEnemy(EnemyTypeId.Base, RandomSpawnPosition(hero.WorldPosition));
                }
            }
        }

        private Vector3 RandomSpawnPosition(Vector3 heroWorldPosition)
        {
            bool startWithHorizontal = Random.Range(0, 2) == 0;

            return startWithHorizontal
                ? HorizontalSpawnPosition(heroWorldPosition)
                : VerticalSpawnPosition(heroWorldPosition);
        }

        private Vector3 HorizontalSpawnPosition(Vector3 heroWorldPosition)
        {
            float[] horizontalDirections = { -1f, 1f };
            float primaryDirection = horizontalDirections.PickRandom();

            float horizontalOffsetDistance = _cameraProvider.WorldScreenWidth / 2 + _spawnerConfig.SpawnDistanceGap;
            float depthRandomOffset = Random.Range(-_cameraProvider.WorldScreenHeight / 2, _cameraProvider.WorldScreenHeight / 2);

            return heroWorldPosition + new Vector3(primaryDirection * horizontalOffsetDistance, 0f, depthRandomOffset);
        }

        private Vector3 VerticalSpawnPosition(Vector3 heroWorldPosition)
        {
            float[] depthDirections = { -1f, 1f };
            float primaryDirection = depthDirections.PickRandom();

            float verticalOffsetDistance = _cameraProvider.WorldScreenHeight / 2 + _spawnerConfig.SpawnDistanceGap;
            float horizontalRandomOffset = Random.Range(-_cameraProvider.WorldScreenWidth / 2, _cameraProvider.WorldScreenWidth / 2);

            return heroWorldPosition + new Vector3(horizontalRandomOffset, 0f, primaryDirection * verticalOffsetDistance);
        }
    }
}
