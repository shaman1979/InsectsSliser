using UnityEngine;
using LightDev;
using Slicer.UI;
using Zenject;
using System;
using MeshSlice;

namespace Slicer.Game
{
    public class LevelsInitializer : IInitializable, IDisposable
    {
        private readonly Setting setting;

        private int totalLevelIndex;
        private int nextMeshIndex;
        private LevelInfo currentLevels;

        private const string levelKey = "level";

        private LevelInfo[] Levels => setting.Levels.levels;

        public LevelsInitializer(Setting setting)
        {
            this.setting = setting;
        }

        public void Dispose()
        {
            Events.PreReset -= OnPreReset;
            Events.GameFinish -= OnGameFinish;
        }

        public void Initialize()
        {
            Events.PreReset += OnPreReset;
            Events.GameFinish += OnGameFinish;

            OnPreReset();
        }

        private void OnPreReset()
        {
            totalLevelIndex = PlayerPrefs.GetInt(levelKey, 0);
            nextMeshIndex = 0;

            currentLevels = LevelSelection();
        }

        private void OnGameFinish()
        {
            if (StarsActivator.HasLevelUp())
                PlayerPrefs.SetInt(levelKey, totalLevelIndex + 1);
        }

        public int GetLevel() => totalLevelIndex + 1;

        public string GetLevelName()
        {
            return currentLevels.name;
        }

        public int GetMeshesCountOnLevel()
        {
            return currentLevels.meshes.Length;
        }

        public bool TryNextMesh(out MeshInfo mesh)
        {
            mesh = null;
            nextMeshIndex++;

            if (nextMeshIndex < GetMeshesCountOnLevel())
            {
                mesh = GetMesh(nextMeshIndex);
                return true;
            }

            return false;
        }

        private MeshInfo GetMesh(int meshIndex)
        {
            return currentLevels.meshes[meshIndex];
        }

        private LevelInfo LevelSelection()
        {
            if (totalLevelIndex >= Levels.Length)
            {
                return Levels[UnityEngine.Random.Range(0, Levels.Length - 1)]; ;
            }

            return Levels[totalLevelIndex];
        }

        [Serializable]
        public class Setting
        {
            public LevelsSettings Levels;
        }
    }
}
