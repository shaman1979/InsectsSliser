using UnityEngine;

using LightDev;
using Zenject;
using System;
using Slicer.EventAgregators;
using Slicer.Sound.Messages;

namespace Slicer.Sound
{
    public class SoundActivator : IInitializable, IDisposable
    {
        private readonly AudioSource source;
        private readonly AudioClip sliceClip;
        private IEventsAgregator eventsAgregator;
        
        public SoundActivator(AudioSource source, [Inject(Id = "Slice")] AudioClip sliceClip, IEventsAgregator agregator)
        {
            eventsAgregator = agregator;
            this.source = source;
            this.sliceClip = sliceClip;
        }

        public void Initialize()
        {
            eventsAgregator.AddListener<SliceSoundPlayMessage>((message) => PlaySound(sliceClip));
        }

        public void Dispose()
        {
        }

        private void PlaySound(AudioClip clip)
        {
            if (source == null || clip == null) return;

            source.clip = clip;
            source.Play();
        }
    }
}
