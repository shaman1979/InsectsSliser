using Slicer.Levels;

namespace Level.Messages
{
    public class CurrentLevelInitializeMessage
    {
        public readonly LevelInfo Level;

        public CurrentLevelInitializeMessage(LevelInfo level)
        {
            Level = level;
        }
    }
}