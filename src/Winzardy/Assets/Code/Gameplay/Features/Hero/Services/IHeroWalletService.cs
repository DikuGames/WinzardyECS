namespace Code.Gameplay.Features.Hero.Services
{
    public interface IHeroWalletService
    {
        int Coins { get; }
        void AddCoins(int amount);
        void Reset();
    }
}
