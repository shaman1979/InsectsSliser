namespace Slicer.HP.Messages
{
    public class MaxProgressMessage
    {
        public readonly int MaxProgress;

        public MaxProgressMessage(int maxProgress)
        {
            MaxProgress = maxProgress;
        }
    }
}