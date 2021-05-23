using System;
using LightDev;
using MeshSlice;
using Slicer.Game;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Tools;
using Slicer.EventAgregators;
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
        private HPInitializer hpInitializer;

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

        private void StartCalculate(Mesh right, Mesh left)
        {
            eventsAgregator.Invoke(new SliceSoundPlayMessage());
            StartCoroutine(CalculateSlicePercentage(right, left));
        }

        private IEnumerator CalculateSlicePercentage(Mesh right, Mesh left)
        {
            yield return StartAreaCalculate(right, left, Calculate);
        }

        private void Calculate(float leftArea, float rightArea)
        {
            var areaSum = leftArea + rightArea;

            var firstPercentage = (int) leftArea.Map(0, areaSum, 0, 100);
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

        private IEnumerator StartAreaCalculate(Mesh negMesh, Mesh posMesh, Action<float, float> onCalculateFinished)
        {
            var vertices = negMesh.vertices;
            var triangles = negMesh.triangles;

            var negResult = 0f;
            for (var p = 0; p < triangles.Length; p += 3)
            {
                negResult += GetArea(vertices[triangles[p + 1]] - vertices[triangles[p]],
                    vertices[triangles[p + 2]] - vertices[triangles[p]]);
                if (p % 50 == 0)
                {
                    yield return new WaitForSeconds(0.01f);
                }
            }
            
            vertices = posMesh.vertices; 
            triangles = posMesh.triangles;
            yield return null;
            
            var posResult = 0f;
            for (var p = 0; p < triangles.Length; p += 3)
            {
                posResult += GetArea(vertices[triangles[p + 1]] - vertices[triangles[p]],
                    vertices[triangles[p + 2]] - vertices[triangles[p]]);

                if (p % 50 == 0)
                {
                     yield return new WaitForSeconds(0.01f);
                }
            }
            
            onCalculateFinished?.Invoke(negResult, posResult);
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