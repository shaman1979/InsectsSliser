using Slicer.Application;
using Slicer.DebugMode.Messages;
using Slicer.EventAgregators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.DebugMode
{
    public class DebugModeActivator : IInitializable
    {
        private readonly ApplicationOptions options;
        private readonly IEventsAgregator eventsAgregator;

        public DebugModeActivator(ApplicationOptions options, IEventsAgregator eventsAgregator)
        {
            this.options = options;
            this.eventsAgregator = eventsAgregator;
        }

        public void Initialize()
        {
            if (options.IsDebugMode)
            {
                eventsAgregator.Invoke(new DebugModeActiveMessage());
            }
            else
            {
                eventsAgregator.Invoke(new DebugModeDeactiveMessage());
            }

            Debug.Assert(!options.IsDebugMode, $"Режим дебага включен");
        }
    }
}