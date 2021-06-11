using System;
using System.Collections;

using UnityEngine;

namespace LightDev.Core
{
    public partial class Base : MonoBehaviour
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }
        
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private protected Coroutine DelayAction(float delay, Action action)
        {
            return StartCoroutine(DelayCoroutine(delay, action));
        }

        private IEnumerator DelayCoroutine(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }
    }
}
