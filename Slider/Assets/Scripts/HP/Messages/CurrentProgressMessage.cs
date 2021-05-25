namespace Slicer.HP.Messages
{
    public class CurrentProgressMessage
    {
        public int Progress { get; }
 
        public CurrentProgressMessage(int progress)
        {
            Progress = progress;
        }
    }
}