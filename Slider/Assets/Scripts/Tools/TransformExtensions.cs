using UnityEngine;

namespace Tools
{
    public static class TransformExtensions
    {
        public static void UnParent(this Transform transform)
        {
            transform.parent = null;
        }

        public static Vector3 GetPosition(this Transform transform)
        {
            if (transform is RectTransform rectTransform)
                return rectTransform.anchoredPosition;
            
            return transform.position;
        }

        public static float GetPositionX(this Transform transform)
        {
            if (transform is RectTransform rectTransform)
                return rectTransform.anchoredPosition.x;

            return transform.position.x;
        }
        
        public static float GetPositionY(this Transform transform)
        {
            if (transform is RectTransform rectTransform)
                return rectTransform.anchoredPosition.y;

            return transform.position.y;
        }
        
        public static float GetPositionZ(this Transform transform)
        {
            if (transform is RectTransform)
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");

            return transform.position.z;
        }

        public static Vector3 GetLocalPosition(this Transform transform)
        {
            if (transform is RectTransform rectTransform)
                return rectTransform.anchoredPosition;

            return transform.localPosition;
        }
        
        public static float GetLocalPositionX(this Transform transform)
        {
            if (transform is RectTransform rectTransform)
                return rectTransform.anchoredPosition.x;

            return transform.localPosition.x;
        }

        public static float GetLocalPositionY(this Transform transform)
        {
            if (transform is RectTransform rectTransform)
                return rectTransform.anchoredPosition.y;

            return transform.localPosition.y;
        }

        public static float GetLocalPositionZ(this Transform transform)
        {
            if (transform is RectTransform)
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");

            return transform.localPosition.z;
        }

        public static Quaternion GetRotation(this Transform transform)
        {
            return transform.rotation;
        }

        public static Quaternion GetLocalRotation(this Transform transform)
        {
            return transform.localRotation;
        }

        public static Vector3 GetEulerRotation(this Transform transform)
        {
            return transform.eulerAngles;
        }

        public static Vector3 GetLocalEulerRotation(this Transform transform)
        {
            return transform.localEulerAngles;
        }
        
        public static float GetEulerRotationX(this Transform transform)
        {
            return transform.eulerAngles.x;
        }

        public static float GetEulerRotationY(this Transform transform)
        {
            return transform.eulerAngles.y;
        }

        public static float GetEulerRotationZ(this Transform transform)
        {
            return transform.eulerAngles.z;
        }

        public static float GetEulerLocalRotationX(this Transform transform)
        {
            return transform.localEulerAngles.x;
        }

        public static float GetEulerLocalRotationY(this Transform transform)
        {
            return transform.localEulerAngles.y;
        }

        public static float GetEulerLocalRotationZ(this Transform transform)
        {
            return transform.localEulerAngles.z;
        }
        
        public static Vector3 Vector3Multiplication(this Vector3 first, Vector3 second)
        {
            return new Vector3(first.x * second.x, first.y * second.y, first.z * second.z);
        }
    }
}