namespace Slicer.HP.Messages
{
    public class CurrentProgressMessage
    {
        public int Progress { get; }
        public int LevelProgress { get; }

        public CurrentProgressMessage(int progress, int levelProgress)
        {
            Progress = progress;
            LevelProgress = levelProgress;
        }
    }
}