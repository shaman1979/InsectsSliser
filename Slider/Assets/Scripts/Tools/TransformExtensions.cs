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
            return transform.GetPosition().x;
        }
        
        public static float GetPositionY(this Transform transform)
        {
            return transform.GetPosition().y;
        }
        
        public static float GetPositionZ(this Transform transform)
        {
            return transform.GetPosition().z;
        }

        public static Vector3 Vector3Multiplication(this Vector3 first, Vector3 second)
        {
            return new Vector3(first.x * second.x, first.y * second.y, first.z * second.z);
        }
    }
}