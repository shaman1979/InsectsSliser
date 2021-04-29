using BzKovSoft.ObjectSlicer;
using DG.Tweening;
using LightDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Slice
{
    public class SlicebleItemMovening : MonoBehaviour
    {
        public event Action OnMoveFinished;

        [SerializeField]
        private GameObject objectToSlice;

        [SerializeField]
        private Transform slicedObjectLeftPos;

        [SerializeField]
        private Transform slicedObjectRightPos;

        private void Awake()
        {
            Events.SliceResult += Movening;
        }

        private void Movening(BzSliceTryResult result)
        {
            RotateAll(result.outObjectPos, result.outObjectNeg, objectToSlice.transform.eulerAngles.y);

            AnimateSlicedObjectMovement(result.outObjectPos, slicedObjectLeftPos.position);
            AnimateSlicedObjectMovement(result.outObjectNeg, slicedObjectRightPos.position, () => OnMoveFinished?.Invoke());

            //int leftPercentage, rightPercentage;
            //CalculateSlicePercentage(left.GetComponent<MeshFilter>().mesh, right.GetComponent<MeshFilter>().mesh, out leftPercentage, out rightPercentage);
            //IncreaseGameProgress(leftPercentage, rightPercentage);
            //Events.SuccessfulSlice.Call(leftPercentage, rightPercentage);
        }

        private void AnimateSlicedObjectMovement(GameObject obj, Vector3 finishPos, Action onFinish = null)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(obj.transform.DOMove(finishPos, 0.5f).SetEase(Ease.InSine));
            sequence.Append(obj.transform.DOMoveY(-4, 0.4f).SetEase(Ease.InSine));
            sequence.Append(DOTween.Sequence().AppendCallback(() =>
            { 
                Destroy(obj);
                onFinish?.Invoke();
            }));
           
        }

        private void RotateAll(GameObject left, GameObject right, float objectToSliceY)
        {
            SetRotationY(left.transform, objectToSliceY);
            SetRotationY(right.transform, objectToSliceY);

            SetRotationY(slicedObjectLeftPos, objectToSliceY);
            SetRotationY(slicedObjectRightPos, objectToSliceY);
        }

        private void SetRotationY(Transform transform, float angle)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }
}