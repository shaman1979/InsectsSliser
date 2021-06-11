using System;
using System.Collections;

using UnityEngine;

namespace LightDev.Core
{
    public partial class Base : MonoBehaviour
    {
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
