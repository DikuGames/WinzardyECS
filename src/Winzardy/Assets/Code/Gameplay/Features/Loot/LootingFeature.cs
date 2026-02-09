using Code.Gameplay.Features.Loot.Systems;
using Code.Infrastructure.Systems;

public sealed class LootingFeature : Feature
{
    public LootingFeature(ISystemFactory systems)
    {
        Add(systems.Create<CastForPullablesSystem>());
        
        Add(systems.Create<PullTowardsHeroSystem>());
        Add(systems.Create<CollectWhenNearSystem>());
        
        Add(systems.Create<CollectEffectItemSystem>());
        Add(systems.Create<AddCoinsOnCollectSystem>());
        
        Add(systems.Create<CleanupCollected>());
    }
}
