using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.Shop.Select
{
    [RequireComponent(typeof(MeshRenderer))]
    public class OutlineDrawer : MonoBehaviour
    {
        private const string RIM_POWER = "_RimPower";

        [SerializeField]
        private float outlinePower = 3.12f;

        [SerializeField]
        private float clearPower = 8f;

        [Inject]
        private MaterialPropertyBlock materialProperty;

        private MeshRenderer meshRenderer;
        
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
            meshRenderer.GetPropertyBlock(materialProperty, 0);
            materialProperty.SetFloat(RIM_POWER, clearPower);
            meshRenderer.SetPropertyBlock(materialProperty, 0);
        }
    }
}