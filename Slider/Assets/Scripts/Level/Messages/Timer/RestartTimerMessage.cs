namespace Level.Messages.Timer
{
    public class RestartTimerMessage
    {
        public int RestartTime { get; }

        public RestartTimerMessage(int restartTime)
        {
            RestartTime = restartTime;
        }
    }
}