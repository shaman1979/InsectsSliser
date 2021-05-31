using Level.Messages.Timer;
using LightDev.UI;
using Slicer.EventAgregators;
using Slicer.Levels.Modifycations.Messages;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Slicer.UI.Windows
{
    public class TimerWindow : CanvasElement
    {
        [SerializeField] private Text timer;

        private IEventsAgregator eventsAgregator;
        
        public void Setup(Text timer)
        {
            this.timer = timer;
        }

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            eventAgregator.AddListener<TimerUpdateMessage>(message => SetTimerText(message.Value));
            eventAgregator.AddListener<TimerWindowActiveMessage>(message => Show());
            eventAgregator.AddListener<TimerWindowDeactiveMessage>(message => Hide());
        }

        public void SetTimerText(int value)
        {
            timer.SetText(value.ToString());
        }
    }
}