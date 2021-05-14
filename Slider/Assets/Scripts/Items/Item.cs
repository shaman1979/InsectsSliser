using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class Item : MonoBehaviour
    {
        private ItemMovening movening;

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Move(Vector3 from, float speed, Action onFinished = null)
        {
            if(movening == null)
            {
                movening = new ItemMovening(transform, speed);
            }

            movening.Move(from, onFinished);
        }
    }
}