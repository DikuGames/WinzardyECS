using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Configs
{
    [CreateAssetMenu(menuName = "Configs/EnemySpawner", fileName = "enemySpawnerConfig")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnTimer { get; private set; } = 7f;
        [field: SerializeField] public float SpawnDistanceGap { get; private set; } = 0.5f;
    }
}
