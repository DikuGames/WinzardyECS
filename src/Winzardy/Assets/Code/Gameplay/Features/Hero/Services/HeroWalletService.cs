namespace Code.Gameplay.Features.Hero.Services
{
    public class HeroWalletService : IHeroWalletService
    {
        public int Coins { get; private set; }

        public void AddCoins(int amount)
        {
            if (amount <= 0)
                return;

            Coins += amount;
        }

        public void Reset()
        {
            Coins = 0;
        }
    }
}
