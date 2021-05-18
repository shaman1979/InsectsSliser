using Assets.Scripts.Tools;
using Slicer.Application;
using Slicer.DebugMode;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.Levels;
using Slicer.Sound;
using UnityEngine;
using Zenject;

namespace Slicer.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelsInitializer.Setting levelInitializerSetting;

        [SerializeField]
        private AudioSource source;

        [SerializeField]
        private AudioClip sliceClip;

        [SerializeField]
        private ApplicationOptions options;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameRestarter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelsInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<HPInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<SoundActivator>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelModification>().AsSingle();

            Container.Bind<AsyncHelper>().FromNewComponentOn(new GameObject(nameof(AsyncHelper))).AsSingle();

            Container.BindInstance(source);
            Container.BindInstance(sliceClip).WithId("Slice");

            Container.BindInstance(options);

            Container.BindInstance(levelInitializerSetting);

            Container.Bind<IEventsAgregator>().To<EventsAgregator>().AsSingle();

            DebugModeInstaller.Install(Container);
        }
    }
}