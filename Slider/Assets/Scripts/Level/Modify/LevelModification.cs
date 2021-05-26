using LightDev;
using Slicer.Game;
using System.Collections;
using System.Collections.Generic;
using Level.Messages;
using Slicer.EventAgregators;
using UnityEngine;
using Zenject;

namespace Slicer.Levels
{
    public class LevelModification : IInitializable
    {
        private readonly LevelsInitializer levelsInitializer;
        private IEventsAgregator eventsAgregator;
        
        public LevelModification(LevelsInitializer levelsInitializer, IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
            this.levelsInitializer = levelsInitializer;
        }

        public void Initialize()
        {
            eventsAgregator.AddListener<CurrentLevelInitializeMessage>(message => ModifycationApply(message.Level));
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