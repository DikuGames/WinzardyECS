using TMPro;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Behaviours
{
    public class EnemyHealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;

        public void SetHp(int hp, int maxHp)
        {
            _healthText.text = $"{hp}/{maxHp}";
        }
    }
}