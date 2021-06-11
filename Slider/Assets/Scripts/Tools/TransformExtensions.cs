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
    }
}