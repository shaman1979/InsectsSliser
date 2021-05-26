using UnityEngine;
using LightDev;
using Slicer.UI;
using Zenject;
using System;
using Level.Messages;
using MeshSlice;
using Slicer.Levels;
using Slicer.Application.Storages;
using Slicer.EventAgregators;

namespace Slicer.Game
{
    public class LevelsInitializer : IInitializable, IDisposable
    {
        private readonly IEventsAgregator eventsAgregator;
        private readonly Setting setting;

        private int totalLevelIndex;
        private int nextMeshIndex;
        private LevelInfo currentLevels;


        private LevelInfo[] Levels => setting.Levels.levels;

        public LevelsInitializer(Setting setting, IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
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
            totalLevelIndex = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL, 0);
            nextMeshIndex = 0;

            currentLevels = LevelSelection();
            eventsAgregator.Invoke(new CurrentLevelInitializeMessage(currentLevels));
        }

        private void OnGameFinish()
        {
            if (StarsActivator.HasLevelUp())
            {
                totalLevelIndex++;
                PlayerPrefs.SetInt(PlayerPrefsKeyStorage.LEVEL, totalLevelIndex);
            }
        }

        public MeshInfo GetFirstMesh()
        {
            return currentLevels.Meshes[0];
        }

        public int GetLevel() => totalLevelIndex + 1;

        public string GetLevelName()
        {
            return currentLevels.Name;
        }

        public int GetMeshesCountOnLevel()
        {
            return currentLevels.Meshes.Length;
        }

        public bool TryNextMesh(out MeshInfo mesh)
        {
            mesh = null;


            if (nextMeshIndex < GetMeshesCountOnLevel())
            {
                mesh = GetMesh(nextMeshIndex);
                nextMeshIndex++;
                return true;
            }

            return false;
        }

        private MeshInfo GetMesh(int meshIndex)
        {
            return currentLevels.Meshes[meshIndex];
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
