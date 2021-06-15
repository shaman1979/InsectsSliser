using UnityEngine;
using LightDev;
using LightDev.UI;
using DG.Tweening;
using Slicer.EventAgregators;
using UI.Elements;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MeshSlice.UI
{
    public class SlicePercantage : CanvasElement
    { 
        [SerializeField]
        private BaseText percantage;

        [SerializeField] private UIFade percantageFade;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            Events.GameStart += Show;
            Events.GameFinish += Hide;
            Events.SuccessfulSlice += OnSuccessfulCut;
        }

        public override void Unsubscribe()
        {
            Events.GameStart -= Show;
            Events.GameFinish -= Hide;
            Events.SuccessfulSlice -= OnSuccessfulCut;
        }

        protected override void OnStartShowing()
        {
            //percantageFade.SetFade(0);
        }

        private void OnSuccessfulCut(int left, int right)
        {
            percantage.SetText($"{GetImpression(Mathf.Abs(left - right))}\n{left}/{right}");
            
            //TODO: Доделать потом
            // percantageFade.Sequence(
            //     percantageFade.Fade(1, 0.2f).SetEase(Ease.InSine),
            //     percantageFade.Delay(1),
            //     percantageFade.Fade(0, 0.3f).SetEase(Ease.InSine)
            // );
        }

        private string[] awesome = {"AWESOME", "BEAUTIFUL", "STUNNING", "CRAZY"};
        private string[] notBad = {"NOT BAD", "GOOD", "LIKE "};
        private string[] bad = {"TRY MORE", "NOT GOOD", "UPS.."};

        private string GetImpression(int delta)
        {
            if (delta < 5)
                return awesome[Random.Range(0, awesome.Length)];
            if (delta < 15)
                return notBad[Random.Range(0, notBad.Length)];

            return bad[Random.Range(0, bad.Length)];
        }
    }
}