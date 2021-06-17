using System;
using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;
using Slicer.EventAgregators;
using UI.Elements;

namespace MeshSlice.UI
{
    public class Splash : CanvasElement
    {
        [SerializeField] private UIFade background;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            Events.RequestSplash += Show;
            Events.PostReset += Hide;
        }

        public override void Unsubscribe()
        {
            Events.RequestSplash -= Show;
            Events.PostReset -= Hide;
        }

        protected override void OnStartShowing()
        {
            background.SetFade(0);
            background.Fade(1, 0.5f, Events.RequestReset.Call);
        }

        protected override void OnStartHiding()
        {
            background.StopFade();
        }
    }
}
