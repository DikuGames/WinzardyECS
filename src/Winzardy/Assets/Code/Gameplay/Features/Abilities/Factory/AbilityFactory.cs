using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Abilities.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly IStaticDataService _staticDataService;

        public AbilityFactory(IIdentifierService identifiers, IStaticDataService staticDataService)
        {
            _identifiers = identifiers;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateProjectileAbility()
        {
            var abilityLevel = _staticDataService.GetAbility(AbilityId.Projectile);
            
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddAbilityId(AbilityId.Projectile)
                .AddCooldown(abilityLevel.Cooldown)
                .With(x => x.isProjectileAbility = true)
                .PutOnCooldown();
        }
    }
}