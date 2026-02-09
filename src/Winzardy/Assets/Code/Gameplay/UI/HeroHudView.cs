using TMPro;
using UnityEngine;

namespace Code.Gameplay.UI
{
    public class HeroHudView : MonoBehaviour, IHeroHudView
    {
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _coinsText;

        public void SetHp(float current, float max)
        {
            _hpText.text = $"HP: {Mathf.CeilToInt(current)}/{Mathf.CeilToInt(max)}";
        }

        public void SetCoins(int coins)
        {
            _coinsText.text = $"Coins: {coins}";
        }
    }
}
