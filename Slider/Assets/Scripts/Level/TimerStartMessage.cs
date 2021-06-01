namespace Slicer.Game
{
    public class TimerStartMessage
    {
        public TimerStartMessage(int startTime)
        {
            StartTime = startTime;
        }

        public int StartTime { get; }
    }
}