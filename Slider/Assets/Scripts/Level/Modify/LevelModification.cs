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
        
        public LevelModification(LevelsInitializer levelsInitializer, IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
            this.levelsInitializer = levelsInitializer;
        }

        public void Initialize()
        {
            eventsAgregator.AddListener<CurrentLevelInitializeMessage>(message => SetCurrentLevel(message.Level));
            eventsAgregator.AddListener<>;
        }

        private void SetCurrentLevel(LevelInfo level) => currentLevel = level;
        
        private void ModifycationApply(LevelInfo level)
        {
            foreach (var modify in level.Modifies)
            {
                modify.Apply(eventsAgregator);
            }
        }
    }
}