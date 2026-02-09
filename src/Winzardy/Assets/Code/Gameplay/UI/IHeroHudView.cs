namespace Code.Gameplay.UI
{
    public interface IHeroHudView
    {
        void SetHp(float current, float max);
        void SetCoins(int coins);
    }
}
