using UnityEngine;

using LightDev;
using Zenject;
using System;

namespace Slicer.Sound
{
    public class SoundActivator : IInitializable, IDisposable
    {
        private readonly AudioSource source;
        private readonly AudioClip sliceClip;

        public SoundActivator(AudioSource source, [Inject(Id = "Slice")] AudioClip sliceClip)
        {
            this.source = source;
            this.sliceClip = sliceClip;
        }

        public void Initialize()
        {
            Events.SuccessfulSlice += OnSuccessfulSlice;
        }

        public void Dispose()
        {
            Events.SuccessfulSlice -= OnSuccessfulSlice;
        }

        private void OnSuccessfulSlice(int left, int right) => PlaySound(sliceClip);

        private void PlaySound(AudioClip clip)
        {
            if (source == null || clip == null) return;

            source.clip = clip;
            source.Play();
        }
    }
}
