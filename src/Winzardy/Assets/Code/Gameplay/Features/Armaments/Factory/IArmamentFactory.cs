using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public interface IArmamentFactory
    {
        GameEntity CreateProjectile(Vector3 at);
    }
}