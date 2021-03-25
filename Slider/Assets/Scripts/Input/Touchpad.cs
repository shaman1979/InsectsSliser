using UnityEngine;
using UnityEngine.EventSystems;

using LightDev;
using Slicer.Shop.Events;

namespace MeshSlice.UI
{
    public class Touchpad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public float sensitivity = 1;
        private float _pixelDelta;

        private void Start()
        {
            ShopEvents.ShopShow += Deactive;
            ShopEvents.ShopHide += Active;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _pixelDelta = eventData.delta.x * 0.001f * sensitivity;
        }

        private void Update()
        {
            InputManager.SetHorizontal(_pixelDelta);
            _pixelDelta = 0;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Events.PointerUp.Call();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Events.PointerDown.Call();
        }

        private void Active()
        {
            gameObject.SetActive(true);
        }

        private void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}
