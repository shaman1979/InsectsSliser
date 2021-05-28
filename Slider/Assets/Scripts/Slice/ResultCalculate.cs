using System;
using LightDev;
using MeshSlice;
using Slicer.Game;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Tools;
using Slicer.EventAgregators;
using Slicer.HP;
using Slicer.Sound.Messages;
using UnityEngine;
using Zenject;

namespace Slicer.Slice
{
    public class ResultCalculate : MonoBehaviour
    {
        [SerializeField]
        private SlicebleItemMovening ItemMovening;

        [Inject]
        private HpInitializer hpInitializer;

        [Inject] private AsyncHelper asyncHelper;

        [Inject] private IEventsAgregator eventsAgregator;
        
        private int deviation = 2;

        private void OnEnable()
        {
            ItemMovening.OnMoveningStarted += StartCalculate;
        }

        private void OnDisable()
        {
            ItemMovening.OnMoveningStarted -= StartCalculate;
        }

        private void StartCalculate(int right, int left)
        {
            eventsAgregator.Invoke(new SliceSoundPlayMessage());
            CalculateSlicePercentage(right, left);
        }

        private void CalculateSlicePercentage(int right, int left)
        {
            Calculate(right, left);
        }

        private void Calculate(float leftArea, float rightArea)
        {
            var areaSum = leftArea + rightArea;

            Debug.Log($"LeftArea {leftArea}, RightArea{rightArea}");
            
            var firstPercentage = (int)((leftArea / areaSum) * 100);
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

        private float GetArea(Vector3 verticeFirst, Vector3 verticeSecond)
        {
            return (Vector3.Cross(
                    verticeFirst,
                    verticeSecond)
                ).magnitude;
        }

        private void IncreaseGameProgress(int leftPercentage, int rightPercentage)
        {
            float percentageDelta = Mathf.Abs(leftPercentage - rightPercentage);
            var koef = (100 - percentageDelta) / 100;
            hpInitializer.IncreaseProgress(koef);
        }
    }
}