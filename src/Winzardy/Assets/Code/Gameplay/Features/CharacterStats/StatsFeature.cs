using Code.Gameplay.Features.CharacterStats.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.CharacterStats
{
    public sealed class StatsFeature : Feature
    {
        public StatsFeature(ISystemFactory systems)
        {
            Add(systems.Create<StatsChangeSystem>());
            
            Add(systems.Create<ApplySpeedFromStatsSystem>());
        }
    }
}