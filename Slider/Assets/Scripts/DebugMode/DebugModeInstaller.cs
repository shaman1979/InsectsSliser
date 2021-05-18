using UnityEngine;
using Zenject;

namespace Slicer.DebugMode
{
    public class DebugModeInstaller : Installer<DebugModeInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DebugModeActivator>().AsSingle();
        }
    } 
}