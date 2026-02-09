using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.Enemies.Configs;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public EnemyFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 at)
        {
            EnemyConfig config = _staticDataService.GetEnemyConfig(typeId);

            return CreateBase(typeId, at, config);
        }

        private GameEntity CreateBase(EnemyTypeId typeId, Vector3 at, EnemyConfig config)
        {
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                .With(x => x[Stats.Speed] = config.Speed)
                .With(x => x[Stats.MaxHp] = config.MaxHp)
                .With(x => x[Stats.Damage] = config.Damage);
                
            return CreateEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddEnemyTypeId(typeId)
                    .AddWorldPosition(at)
                    .AddDirection(Vector2.zero)
                    .AddBaseStats(baseStats)
                    .AddStatModifiers(InitStats.EmptyStatDictionary())
                    .AddSpeed(baseStats[Stats.Speed])
                    .AddCurrentHp(baseStats[Stats.MaxHp])
                    .AddMaxHp(baseStats[Stats.MaxHp])
                    .AddEffectSetups(new List<EffectSetup> {new(){EffectTypeId = EffectTypeId.Damage, Value = baseStats[Stats.Damage]}})
                    .AddTargetsBuffer(new List<int>(1))
                    .AddCollectTargetsInterval(0.5f)
                    .AddCollectTargetsTimer(0f)
                    .AddRadius(config.AttackRadius)
                    .AddLayerMask(CollisionLayer.Enemy.AsMask())
                    .AddViewPrefab(config.ViewPrefab)
                    .With(x => x.isEnemy = true)
                    .With(x => x.isTurnedAlongDirection = true)
                    .With(x => x.isMovementAvailable = true)
                ;
        }
    }
}
