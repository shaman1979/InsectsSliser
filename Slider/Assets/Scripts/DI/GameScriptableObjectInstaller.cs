using MeshSlice;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.DI
{
    [CreateAssetMenu(fileName = "GameScriptableObjectInstaller", menuName = "Installers/GameScriptableObjectInstaller")]
    public class GameScriptableObjectInstaller : ScriptableObjectInstaller<GameScriptableObjectInstaller>
    {
        [SerializeField, ReadOnly]
        private LevelsSettings levelsSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(levelsSettings);
        }
    }
}