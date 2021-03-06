﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace LightDev.Core
{
    public class SequenceHelper : IDisposable
    {
        private readonly List<Sequence> sequences = new List<Sequence>();
        private Transform transform;

        public SequenceHelper(Transform transform)
        {
            this.transform = transform;
        }

        public void Dispose()
        {
            KillSequences();
        }

        public virtual Sequence Sequence(params Tween[] tweens)
        {
            var sequence = DOTween.Sequence();
            if (transform.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                sequence.SetUpdate(true);
            }
            foreach (var tween in tweens)
            {
                sequence.Append(tween);
            }

            sequence.OnKill(() =>
            {
                sequences.Remove(sequence);
            });

            sequences.Add(sequence);

            return sequence;
        }

        public virtual void PauseSequence(string id)
        {
            for (var i = 0; i < sequences.Count; i++)
            {
                if (sequences[i].stringId != null && sequences[i].stringId.Equals(id))
                {
                    sequences[i].Pause();
                }
            }
        }

        public virtual void PauseSequences()
        {
            foreach (var sequence in sequences)
            {
                sequence.Pause();
            }
        }

        public virtual void ResumeSequence(string id)
        {
            for (var i = 0; i < sequences.Count; i++)
            {
                if (sequences[i].stringId != null && sequences[i].stringId.Equals(id))
                {
                    sequences[i].Play();
                }
            }
        }

        public virtual void ResumeSequences()
        {
            foreach (var sequence in sequences)
            {
                sequence.Play();
            }
        }

        public virtual void KillSequence(string id, bool complete = false)
        {
            for (var i = sequences.Count - 1; i >= 0; i--)
            {
                if (sequences[i].stringId != null && sequences[i].stringId.Equals(id))
                {
                    var sequence = sequences[i];
                    sequences.RemoveAt(i);
                    sequence.Kill(complete);
                }
            }
        }

        public virtual void KillSequences(bool complete = false)
        {
            for (var i = sequences.Count - 1; i >= 0; i--)
            {
                sequences[i].Kill(complete);
            }

            sequences.Clear();
        }

        public virtual bool ContainsSequence(string id)
        {
            for (var i = 0; i < sequences.Count; i++)
            {
                if (sequences[i].stringId != null && sequences[i].stringId.Equals(id))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool IsAnySequenceRunning()
        {
            return sequences.Count != 0;
        }

        public virtual Tween Delay(float delay)
        {
            return DOTween.Sequence().AppendInterval(delay);
        }

        public virtual Tween OnFinish(TweenCallback tweenCallback)
        {
            return DOTween.Sequence().AppendCallback(tweenCallback);
        }

        public virtual Tween Move(Vector3 endValue, float duration)
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).DOAnchorPos(endValue, duration);

            return transform.DOMove(endValue, duration);
        }

        public virtual Tween MoveX(float endValue, float duration)
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).DOAnchorPosX(endValue, duration);

            return transform.DOMoveX(endValue, duration);
        }

        public virtual Tween MoveY(float endValue, float duration)
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).DOAnchorPosY(endValue, duration);

            return transform.DOMoveY(endValue, duration);
        }

        public virtual Tween MoveZ(float endValue, float duration)
        {
            if (transform is RectTransform)
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");

            return transform.DOMoveZ(endValue, duration);
        }

        public virtual Tween MoveLocal(Vector3 endValue, float duration)
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).DOAnchorPos(endValue, duration);

            return transform.DOLocalMove(endValue, duration);
        }

        public virtual Tween MoveLocalX(float endValue, float duration)
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).DOAnchorPosX(endValue, duration);

            return transform.DOLocalMoveX(endValue, duration);
        }

        public virtual Tween MoveLocalY(float endValue, float duration)
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).DOAnchorPosY(endValue, duration);

            return transform.DOLocalMoveY(endValue, duration);
        }

        public virtual Tween MoveLocalZ(float endValue, float duration)
        {
            if (transform is RectTransform)
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");

            return transform.DOLocalMoveZ(endValue, duration);
        }

        public virtual Tween Rotate(Quaternion endValue, float duration)
        {
            return transform.DORotateQuaternion(endValue, duration);
        }

        public virtual Tween Rotate(Vector3 endValue, float duration)
        {
            return transform.DORotate(endValue, duration);
        }

        public virtual Tween RotateX(float endValue, float duration)
        {
            return transform.DORotate(new Vector3(endValue, transform.GetEulerRotationY(), transform.GetEulerRotationZ()), duration);
        }

        public virtual Tween RotateY(float endValue, float duration)
        {
            return transform.DORotate(new Vector3(transform.GetEulerRotationX(), endValue, transform.GetEulerRotationZ()), duration);
        }

        public virtual Tween RotateZ(float endValue, float duration)
        {
            return transform.DORotate(new Vector3(transform.GetEulerRotationX(), transform.GetEulerRotationY(), endValue), duration);
        }

        public virtual Tween RotateLocal(Quaternion endValue, float duration)
        {
            return transform.DOLocalRotateQuaternion(endValue, duration);
        }

        public virtual Tween RotateLocal(Vector3 endValue, float duration)
        {
            return transform.DOLocalRotate(endValue, duration);
        }

        public virtual Tween RotateLocalX(float endValue, float duration)
        {
            return transform.DOLocalRotate(new Vector3(endValue, transform.GetEulerLocalRotationX(), transform.GetEulerLocalRotationZ()), duration);
        }

        public virtual Tween RotateLocalY(float endValue, float duration)
        {
            return transform.DOLocalRotate(new Vector3(transform.GetEulerLocalRotationX(), endValue,transform.GetEulerLocalRotationZ()), duration);
        }

        public virtual Tween RotateLocalZ(float endValue, float duration)
        {
            return transform.DOLocalRotate(new Vector3(transform.GetEulerLocalRotationX(), transform.GetEulerLocalRotationY(), endValue), duration);
        }

        public virtual Tween Scale(Vector3 endValue, float duration)
        {
            return transform.DOScale(endValue, duration);
        }

        public virtual Tween Scale(float endValue, float duration)
        {
            return Scale(new Vector3(endValue, endValue, endValue), duration);
        }

        public virtual Tween ScaleX(float endValue, float duration)
        {
            return transform.DOScaleX(endValue, duration);
        }

        public virtual Tween ScaleY(float endValue, float duration)
        {
            return transform.DOScaleY(endValue, duration);
        }

        public virtual Tween ScaleZ(float endValue, float duration)
        {
            return transform.DOScaleZ(endValue, duration);
        }

        public virtual Tween Fade(float endValue, float duration)
        {
            if (transform.GetComponent<Image>())
            {
                return transform.GetComponent<Image>().DOFade(endValue, duration);
            }

            if (transform.GetComponent<Text>())
            {
                return transform.GetComponent<Text>().DOFade(endValue, duration);
            }

            throw new NotImplementedException();
        }

        public virtual Tween Color(Color endValue, float duration)
        {
            if (transform.GetComponent<Image>())
            {
                return transform.GetComponent<Image>().DOColor(endValue, duration);
            }

            if (transform.GetComponent<Text>())
            {
                return transform.GetComponent<Text>().DOColor(endValue, duration);
            }

            throw new NotImplementedException();
        }
    }
}
