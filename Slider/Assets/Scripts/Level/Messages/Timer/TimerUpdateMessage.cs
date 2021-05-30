namespace Slicer.Levels.Modifycations.Messages
{
    public class TimerUpdateMessage
    {
        public readonly int Value;
        
        public TimerUpdateMessage(int timerValue)
        {
            Value = timerValue;
        }
    }
}