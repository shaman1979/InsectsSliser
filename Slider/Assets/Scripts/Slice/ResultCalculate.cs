using System;
using LightDev;
using MeshSlice;
using Slicer.Game;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Tools;
using Slicer.EventAgregators;
using Slicer.HP;
using Slicer.Logger;
using Slicer.Slice.Messages;
using Slicer.Sound.Messages;
using Slicer.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Slicer.Slice
{
    public class ResultCalculate : MonoBehaviour
    {
        public event Action<float> OnProgressCalculateEnded;
    
        [FormerlySerializedAs("ItemMovening")] [SerializeField]
        private SlicebleItemMovening itemMovening;
        
        private IEventsAgregator eventsAgregator;
        
        private int deviation = 2;

        [Inject]
        public void Setup(SlicebleItemMovening itemMovening, IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
            this.itemMovening = itemMovening;
        }
        
        private void Start()
        {
            itemMovening.OnMoveningStarted += StartCalculate;
        }

        public void StartCalculate(int right, int left)
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

            if (areaSum.IsZero().AssertTry($"Сумма мешей не может ровняться нулю"))
                return;

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
            eventsAgregator.Invoke(new ResultPercentageMessage(firstPercentage, secondPercentage));
            Events.SuccessfulSlice.Call(firstPercentage, secondPercentage);
        }

        public void IncreaseGameProgress(int leftPercentage, int rightPercentage)
        {
            float percentageDelta = Mathf.Abs(leftPercentage - rightPercentage);
            percentageDelta = (100 - percentageDelta) / 100;
            OnProgressCalculateEnded?.Invoke(percentageDelta);
        }
    }
}