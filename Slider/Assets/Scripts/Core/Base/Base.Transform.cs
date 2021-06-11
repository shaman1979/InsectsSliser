using UnityEngine;

namespace LightDev.Core
{
    public partial class Base
    {
        public float GetPositionX()
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).anchoredPosition.x;

            return transform.position.x;
        }

        public float GetPositionY()
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).anchoredPosition.y;

            return transform.position.y;
        }

        public float GetPositionZ()
        {
            if (transform is RectTransform)
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");

            return transform.position.z;
        }

        public Vector3 GetLocalPosition()
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).anchoredPosition;

            return transform.localPosition;
        }

        public float GetLocalPositionX()
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).anchoredPosition.x;

            return transform.localPosition.x;
        }

        public float GetLocalPositionY()
        {
            if (transform is RectTransform)
                return ((RectTransform)transform).anchoredPosition.y;

            return transform.localPosition.y;
        }

        public float GetLocalPositionZ()
        {
            if (transform is RectTransform)
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");

            return transform.localPosition.z;
        }

        public Quaternion GetRotation()
        {
            return transform.rotation;
        }

        public Quaternion GetLocalRotation()
        {
            return transform.localRotation;
        }

        public Vector3 GetEulerRotation()
        {
            return transform.eulerAngles;
        }

        public Vector3 GetLocalEulerRotation()
        {
            return transform.localEulerAngles;
        }

        public float GetRotationX()
        {
            return transform.eulerAngles.x;
        }

        public float GetRotationY()
        {
            return transform.eulerAngles.y;
        }

        public float GetRotationZ()
        {
            return transform.eulerAngles.z;
        }

        public float GetLocalRotationX()
        {
            return transform.localEulerAngles.x;
        }

        public float GetLocalRotationY()
        {
            return transform.localEulerAngles.y;
        }

        public float GetLocalRotationZ()
        {
            return transform.localEulerAngles.z;
        }

        public Vector3 GetScale()
        {
            return transform.localScale;
        }

        public float GetScaleX()
        {
            return transform.localScale.x;
        }

        public float GetScaleY()
        {
            return transform.localScale.y;
        }

        public float GetScaleZ()
        {
            return transform.localScale.z;
        }

        public void SetPosition(Vector3 position)
        {
            if (transform is RectTransform)
            {
                ((RectTransform)transform).anchoredPosition = position;
            }
            else
            {
                transform.position = position;
            }
        }

        public void SetPositionX(float x)
        {
            if (transform is RectTransform)
            {
                RectTransform rectTransform = (RectTransform)transform;
                rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
            }
            else
            {
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
        }

        public void SetPositionY(float y)
        {
            if (transform is RectTransform)
            {
                RectTransform rectTransform = (RectTransform)transform;
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
        }

        public void SetPositionZ(float z)
        {
            if (transform is RectTransform)
            {
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, z);
            }
        }

        public void SetLocalPosition(Vector3 localPosition)
        {
            if (transform is RectTransform)
            {
                ((RectTransform)transform).anchoredPosition = localPosition;
            }
            else
            {
                transform.localPosition = localPosition;
            }
        }

        public void SetLocalPositionX(float x)
        {
            if (transform is RectTransform)
            {
                RectTransform rectTransform = (RectTransform)transform;
                rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
            }
            else
            {
                transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
            }
        }

        public void SetLocalPositionY(float y)
        {
            if (transform is RectTransform)
            {
                RectTransform rectTransform = (RectTransform)transform;
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
            }
        }

        public void SetLocalPositionZ(float z)
        {
            if (transform is RectTransform)
            {
                throw new System.NotImplementedException("RectTransfrom does not have anchoredPosition.z");
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
            }
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public void SetRotation(Vector3 eulerAngles)
        {
            transform.eulerAngles = eulerAngles;
        }

        public void SetLocalRotation(Quaternion rotation)
        {
            transform.localRotation = rotation;
        }

        public void SetLocalRotation(Vector3 eulerAngles)
        {
            transform.localEulerAngles = eulerAngles;
        }

        public void SetRotationX(float x)
        {
            transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        public void SetRotationY(float y)
        {
            transform.rotation = Quaternion.AngleAxis(y, Vector3.up);

            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
        }

        public void SetRotationZ(float z)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
        }

        public void SetLocalRotationX(float x)
        {
            transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        public void SetLocalRotationY(float y)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
        }

        public void SetLocalRotationZ(float z)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        public void SetScale(float scale)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }

        public void SetScale(float x, float y, float z)
        {
            transform.localScale = new Vector3(x, y, z);
        }

        public void SetScaleX(float x)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }

        public void SetScaleY(float y)
        {
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }

        public void SetScaleZ(float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
        }
    }
}
