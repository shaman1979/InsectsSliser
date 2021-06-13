using System.Collections;
using System.Collections.Generic;
using Applications.Messages;
using UnityEngine;
using LightDev;
using LightDev.Core;
using LightDev.UI;
using DG.Tweening;
using Slicer.EventAgregators;
using Tools;

namespace MeshSlice.UI
{
    public class DragToControl : CanvasElement
    {
        [Header("References")] public Base holder;
        public Base finger;

        private int clickTime;

        public override void Subscribe(IEventsAgregator eventsAgregator)
        {
            Events.GameStart += Show;
            Events.PointerDown += OnPointerUp;
            eventsAgregator.AddListener<GameFinishMessage>(message => Hide());
        }

        public override void Unsubscribe()
        {
            Events.GameStart -= Show;
            Events.PointerDown -= OnPointerUp;
        }

        private void OnPointerUp()
        {
            Hide();
        }

        protected override void OnStartShowing()
        {
            holder.gameObject.Deactivate();
        }

        protected override void OnFinishShowing()
        {
            holder.gameObject.Activate();
            AnimateFinger();
        }

        private void AnimateFinger()
        {
            //TODO: Доделать позже
            // finger.SetPositionX(-153);
            // finger.Sequence(
            //     finger.MoveX(213, 1.2f).SetEase(Ease.InOutQuart),
            //     finger.MoveX(-153, 1.2f).SetEase(Ease.InOutQuart)
            // ).SetLoops(-1);
        }
    }
}