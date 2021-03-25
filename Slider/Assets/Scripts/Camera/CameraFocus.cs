using MeshSlice;
using Slicer.Shop;
using Slicer.Shop.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slicer.Camera
{
    [RequireComponent(typeof(CameraStateChanger))]
    public class CameraFocus : MonoBehaviour
    {
        [SerializeField]
        private Focus[] focuses;

        private CameraStateChanger stateChanger;

        private void Awake()
        {
            ShopEvents.ItemTypeChanged += ChangeFocus;

            stateChanger = GetComponent<CameraStateChanger>();
        }

        private void ChangeFocus(ItemTypes type)
        {
            var focus = focuses.FirstOrDefault(x => x.Type == type);

            if (focus == null)
            {
                Debug.LogError($"Камера не может сфокусировать на объекте с типом {type}");
                return;
            }

            stateChanger.ChangeState(focus.State);
        }

        [System.Serializable]
        public class Focus
        {
            public ItemTypes Type;
            public CameraState State;
        }
    }
}