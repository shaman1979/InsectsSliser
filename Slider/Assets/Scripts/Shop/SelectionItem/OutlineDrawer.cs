using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.Shop.Select
{
    [RequireComponent(typeof(Outline))]
    public class OutlineDrawer : MonoBehaviour
    {        
        private float outlinePower = 2f;       
        private float clearPower = 0f;
        private Outline outline;
        private Color outLineColor = Color.green;

        private Outline Outline
        {
            get 
            {
                if(outline == null)
                {
                    outline = GetComponent<Outline>();
                }

                return outline;
            }
        }

        private void Start()
        {
            Outline.OutlineColor = outLineColor;
        }

        public void Draw()
        {
            ChangePower(outlinePower);
        }

        public void Clear()
        {
            ChangePower(clearPower);
        }

        private void ChangePower(float power)
        {
            Outline.ChangeWidth(power);
        }
    }
}