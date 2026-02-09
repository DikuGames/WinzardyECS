using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public class ArmamentFactory : IArmamentFactory
    {
        private const int TargetsBufferSize = 16;
        private readonly IIdentifierService _identifiers;
        private readonly IStaticDataService _staticDataService;

        public ArmamentFactory(IIdentifierService identifiers, IStaticDataService staticDataService)
        {
            _identifiers = identifiers;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateProjectile(Vector3 at)
        {
            AbilityData abilityData = _staticDataService.GetAbility(AbilityId.Projectile);
            ProjectileSetup setup = abilityData.ProjectileSetup;

            return CreateProjectileEntity(at, abilityData, setup)
                .AddParentAbility(AbilityId.Projectile)
                .With(x => x.isRotationAlignedAlongDirection = true);
        }

        private GameEntity CreateProjectileEntity(Vector3 at, AbilityData abilityData, ProjectileSetup setup)
        {
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .With(x => x.isArmament = true)
                .AddViewPrefab(abilityData.ViewPrefab)
                .AddWorldPosition(at)
                .AddSpeed(setup.Speed)
                .With(x => x.AddEffectSetups(abilityData.EffectSetups), when: !abilityData.EffectSetups.IsNullOrEmpty())
                .AddRadius(setup.ContactRadius)
                .AddTargetsBuffer(new List<int>(TargetsBufferSize))
                .AddProcessedTargets(new List<int>(TargetsBufferSize))
                .With(x => x.AddTargetLimit(setup.Pierce), when: setup.Pierce > 0)
                .AddLayerMask(CollisionLayer.Enemy.AsMask())
                .With(x => x.isMovementAvailable = true)
                .With(x => x.isReadyToCollectTargets = true)
                .With(x => x.isCollectingTargetsContinuously = true)
                .AddSelfDestructTimer(setup.Lifetime);
        }
    }
}