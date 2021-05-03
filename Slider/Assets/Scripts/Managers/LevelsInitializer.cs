using UnityEngine;
using LightDev;
using Slicer.UI;
using Zenject;
using System;
using MeshSlice;

namespace Slicer.Game
{
    public class LevelsInitializer : IInitializable, IDisposable, IValidatable
    {
        private readonly Setting setting;

        private int currentLevelIndex;
        private int nextMeshIndex;
        private int randomMeshesCount;
        private ExlusiveRandom<Mesh> meshRandomer;

        private const string levelKey = "level";

        private LevelInfo[] Levels
        {
            get
            {
                return setting.Levels.levels;
            }
        }

        public LevelsInitializer(Setting setting)
        {
            this.setting = setting;
        }

        public void Validate()
        {
            setting.MinRandomMeshes = Mathf.Max(setting.MinRandomMeshes, 1);
            setting.MaxRandomMeshes = Mathf.Max(setting.MaxRandomMeshes, setting.MinRandomMeshes);
        }

        public void Dispose()
        {
            Events.PreReset -= OnPreReset;
            Events.GameFinish -= OnGameFinish;
        }

        public void Initialize()
        {
            meshRandomer = new ExlusiveRandom<Mesh>(setting.RandomMeshes);
            currentLevelIndex = PlayerPrefs.GetInt(levelKey, 0);

            Events.PreReset += OnPreReset;
            Events.GameFinish += OnGameFinish;
        }

        private void OnPreReset()
        {          
            randomMeshesCount = UnityEngine.Random.Range(setting.MinRandomMeshes, setting.MaxRandomMeshes + 1);
            nextMeshIndex = 0;
        }

        private void OnGameFinish()
        {
            if(StarsActivator.HasLevelUp())
                PlayerPrefs.SetInt(levelKey, currentLevelIndex + 1);
        }

        public int GetLevel()
        {
            return currentLevelIndex + 1;
        }

        public int GetMeshesCountOnLevel()
        {
            return IsRandomLevel() ? randomMeshesCount : GetCurrentLevelMeshes().Length;
        }

        public bool HasNextMesh()
        {
            return nextMeshIndex != GetMeshesCountOnLevel();
        }

        public MeshInfo GetNextMeshInfo()
        {
            nextMeshIndex++;
            return IsRandomLevel()
                ? new MeshInfo(meshRandomer.GetNext(), new Vector3(0, UnityEngine.Random.Range(0, 360), 0)) 
                :GetCurrentLevelMeshes()[nextMeshIndex - 1];
        }

        private MeshInfo[] GetCurrentLevelMeshes()
        {
            return Levels[currentLevelIndex % Levels.Length].meshes;
        }

        private bool IsRandomLevel()
        {
            return currentLevelIndex >= Levels.Length;
        }

        [Serializable]
        public class Setting
        {
            public LevelsSettings Levels;

            public int MinRandomMeshes = 4;

            public int MaxRandomMeshes = 5;

            public Mesh[] RandomMeshes;
        }
    }
}
