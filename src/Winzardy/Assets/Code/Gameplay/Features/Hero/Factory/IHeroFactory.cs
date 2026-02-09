using UnityEngine;

namespace Code.Gameplay.Features.Hero.Factory
{
    public interface IHeroFactory
    {
        GameEntity Create(Vector3 at);
    }
}