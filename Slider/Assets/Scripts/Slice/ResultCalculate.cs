using LightDev;
using MeshSlice;
using Slicer.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.Slice
{
    public class ResultCalculate : MonoBehaviour
    {
        [SerializeField]
        private SlicebleItemMovening ItemMovening;

        [Inject]
        private HPInitializer hpInitializer;

        private int deviation = 2;

        private void OnEnable()
        {
            ItemMovening.OnMoveningStarted += CalculateSlicePercentage;
        }

        private void OnDisable()
        {
            ItemMovening.OnMoveningStarted -= CalculateSlicePercentage;
        }

        private void CalculateSlicePercentage(Mesh right, Mesh left)
        {
            float leftArea = left.GetArea();
            float rightArea = right.GetArea();
            float areaSum = leftArea + rightArea;

            var firstPercentage = (int)leftArea.Map(0, areaSum, 0, 100);
            var secondPercentage = 100 - firstPercentage;

            if (firstPercentage < secondPercentage)
            {
                firstPercentage = Mathf.Clamp(firstPercentage + deviation, 0, 50);
                secondPercentage = Mathf.Clamp(secondPercentage - deviation, 50, 100);
            }
            else
            {
                secondPercentage = Mathf.Clamp(secondPercentage + deviation, 0, 50);
                firstPercentage = Mathf.Clamp(firstPercentage - deviation, 50, 100);
            }

            IncreaseGameProgress(firstPercentage, secondPercentage);
            Events.SuccessfulSlice.Call(firstPercentage, secondPercentage);
        }

        private void IncreaseGameProgress(int leftPercentage, int rightPercentage)
        {
            float percentageDelta = Mathf.Abs(leftPercentage - rightPercentage);
            float koef = (100 - percentageDelta) / 100;
            hpInitializer.IncreaseProgress(koef);
        }
    }
}