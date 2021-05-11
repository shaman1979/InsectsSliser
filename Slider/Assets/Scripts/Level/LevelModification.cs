using LightDev;
using Slicer.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.Levels
{
    public class LevelModification : IInitializable
    {
        private readonly LevelsInitializer levelsInitializer;

        public LevelModification(LevelsInitializer levelsInitializer)
        {
            this.levelsInitializer = levelsInitializer;
        }

        public void Initialize()
        {
            levelsInitializer.OnLevelChanged += ModifycationApply;
        }

        private void ModifycationApply(LevelInfo level)
        {
            foreach (var modify in level.Modifies)
            {
                modify.Apply();
            }
        }
    }
}