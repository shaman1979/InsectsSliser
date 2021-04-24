using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slicer.UI
{
    public class StarView : MonoBehaviour
    {
        [SerializeField]
        private Image star;

        private bool isReached = false;

        public void StarReached()
        {
            if (isReached)
                return;

            star.transform.DOScale(1.3f, 0.5f).SetEase(Ease.InFlash);
            star.color = Color.yellow;
            isReached = true;
        }

        public void ResetState()
        {
            star.transform.DOScale(1f, 0f);
            star.color = Color.white;
            isReached = false;
        }
    }
}