namespace Level.Messages.Timer
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