using Slicer.EventAgregators;
using Slicer.Levels.Modifycations.Messages;

namespace Slicer.Levels
{
    public class LevelTimerActivatorModify : ILevelModify
    {
        private readonly IEventsAgregator eventsAgregator;

        public LevelTimerActivatorModify(IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
        }

        public void Apply()
        {
            eventsAgregator.Invoke(new TimerWindowActiveMessage());
        }
    }
}