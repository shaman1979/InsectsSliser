using System.Collections.Generic;
using Applications.Messages;
using Slicer.Game;
using Level.Messages;
using Slicer.EventAgregators;
using Zenject;

namespace Slicer.Levels
{
    public class LevelModification : IInitializable
    {
        private readonly LevelsInitializer levelsInitializer;
        private IEventsAgregator eventsAgregator;

        private LevelInfo currentLevel;
        private List<ILevelModify> modifies = new List<ILevelModify>();
        
        public LevelModification(LevelsInitializer levelsInitializer, IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
            this.levelsInitializer = levelsInitializer;
        }

        public void Initialize()
        {
            eventsAgregator.AddListener<CurrentLevelInitializeMessage>(message => SetCurrentLevel(message.Level));
            eventsAgregator.AddListener<ModifucationActiveMessage>(message => ModifycationApply(currentLevel));
            eventsAgregator.AddListener<GameFinishMessage>(message => ModificationDispose());
        }

        private void ModificationDispose()
        {
            modifies.ForEach(x => x.Dispose());
            modifies.Clear();
        }

        private void SetCurrentLevel(LevelInfo level) => currentLevel = level;
        
        private void ModifycationApply(LevelInfo level)
        {
            foreach (var modify in level.Modifies)
            {
                modifies.Add(modify);
                modify.Apply(eventsAgregator);
            }
        }
    }
}