using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slicer.UI
{
    public class StarView : MonoBehaviour
    {
        [SerializeField] private Image star;

        [SerializeField] private float oneStarFill;
        [SerializeField] private float twoStarFill;
        [SerializeField] private float thirdStarFill;

        public void StarReached(int starCount)
        {
            star.transform.DOScale(1.3f, 0.5f).SetEase(Ease.InFlash);
            star.color = Color.yellow;
            
            //TODO: Вообще не нравится решение, потом переделать
            switch (starCount)
            {
                case 0:
                    star.fillAmount = 0;
                    break;
                case 1:
                    star.fillAmount = oneStarFill;
                    break;
                case 2:
                    star.fillAmount = twoStarFill;
                    break;
                case 3:
                    star.fillAmount = thirdStarFill;
                    break;
            }
        }

        public void ResetState()
        {
            star.transform.DOScale(1f, 0f);
            star.color = Color.white;
        }
    }
}