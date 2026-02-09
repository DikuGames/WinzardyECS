using Code.Gameplay.Features.Hero.Services;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class AddCoinsOnCollectSystem : IExecuteSystem
    {
        private const int CoinValue = 1;
        private readonly IHeroWalletService _heroWallet;
        private readonly IGroup<GameEntity> _collectedCoins;

        public AddCoinsOnCollectSystem(GameContext game, IHeroWalletService heroWallet)
        {
            _heroWallet = heroWallet;
            _collectedCoins = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.LootTypeId));
        }

        public void Execute()
        {
            foreach (GameEntity loot in _collectedCoins)
            {
                if (loot.LootTypeId == LootTypeId.Coin)
                    _heroWallet.AddCoins(CoinValue);
            }
        }
    }
}
