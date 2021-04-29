using BzKovSoft.ObjectSlicer;
using DG.Tweening;
using LightDev;
using System;
using UnityEngine;

namespace Slicer.Slice
{
    public class SlicebleItemMovening : MonoBehaviour
    {
        public event Action OnMoveFinished;
        public event Action<Mesh, Mesh> OnMoveningStarted;

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
            var left = result.outObjectNeg;
            var right = result.outObjectPos;

            OnMoveningStarted?.Invoke(GetMesh(left), GetMesh(right));

            RotateAll(left, right, objectToSlice.transform.eulerAngles.y);

            AnimateSlicedObjectMovement(left, slicedObjectLeftPos.position);
            AnimateSlicedObjectMovement(right, slicedObjectRightPos.position, () => OnMoveFinished?.Invoke());
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

        private Mesh GetMesh(GameObject item)
        {
            return item.GetComponent<MeshFilter>().mesh;
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