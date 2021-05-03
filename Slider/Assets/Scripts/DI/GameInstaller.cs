using Slicer.Game;
using UnityEngine;
using Zenject;

namespace Slicer.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelsInitializer.Setting levelInitializerSetting;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameRestarter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelsInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<HPInitializer>().AsSingle();

            Container.BindInstances(levelInitializerSetting);
        }
    }
}