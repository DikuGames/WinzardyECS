using Code.Gameplay.Features.Hero.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Hero
{
    public class HeroFeature : Feature
    {
        public HeroFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetHeroDirectionByInput>());
            Add(systems.Create<UpdateHeroHudSystem>());
            Add(systems.Create<HeroDeathSystem>());
            
            Add(systems.Create<FinalizeHeroDeathProcessingSystem>());
        }
    }
}
