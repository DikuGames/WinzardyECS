using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.Hero.Configs;
using Code.Gameplay.Features.Hero.Services;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly IHeroWalletService _heroWallet;
        private readonly HeroConfig _config;

        public HeroFactory(
            IIdentifierService identifiers,
            IStaticDataService staticDataService,
            IHeroWalletService heroWallet)
        {
            _identifiers = identifiers;
            _heroWallet = heroWallet;
            _config = staticDataService.GetHeroConfig();
        }

        public GameEntity Create(Vector3 at)
        {
            _heroWallet.Reset();

            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                .With(x => x[Stats.Speed] = _config.Speed)
                .With(x => x[Stats.MaxHp] = _config.MaxHp);
            
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddWorldPosition(at)
                .AddDirection(Vector2.zero)
                .AddBaseStats(baseStats)
                .AddStatModifiers(InitStats.EmptyStatDictionary())
                .AddSpeed(baseStats[Stats.Speed])
                .AddMoveBlockerRadius(_config.MoveBlockerRadius)
                .AddCurrentHp(baseStats[Stats.MaxHp])
                .AddMaxHp(baseStats[Stats.MaxHp])
                .AddViewPrefab(_config.ViewPrefab)
                .AddPickupRadius(_config.PickupRadius)
                .With(x => x.isHero = true)
                .With(x => x.isTurnedAlongDirection = true)
                .With(x => x.isMovementAvailable = true)
                ;
        }
    }
}
