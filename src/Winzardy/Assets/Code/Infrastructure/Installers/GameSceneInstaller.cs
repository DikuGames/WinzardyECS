using Code.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private HeroHudView _heroHudView;

        public override void InstallBindings()
        {
            Container.Bind<IHeroHudView>().FromInstance(_heroHudView).AsSingle();
        }
    }
}
