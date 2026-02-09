using Code.Gameplay.Features.Hero.Services;
using Code.Gameplay.UI;
using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class UpdateHeroHudSystem : IExecuteSystem
    {
        private readonly IHeroHudView _heroHud;
        private readonly IHeroWalletService _heroWallet;
        private readonly IGroup<GameEntity> _heroes;

        public UpdateHeroHudSystem(
            GameContext game,
            IHeroHudView heroHud,
            IHeroWalletService heroWallet)
        {
            _heroHud = heroHud;
            _heroWallet = heroWallet;
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.CurrentHp,
                    GameMatcher.MaxHp));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                _heroHud.SetHp(hero.CurrentHp, hero.MaxHp);
                _heroHud.SetCoins(_heroWallet.Coins);
                break;
            }
        }
    }
}
