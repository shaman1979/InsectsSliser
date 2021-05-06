using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class AsyncHelper : MonoBehaviour
    {
        public void Run(Action handler, float delay)
        {
            StartCoroutine(Delay(handler, delay));
        }

        private IEnumerator Delay(Action handler, float delay)
        {
            yield return new WaitForSeconds(delay);
            handler?.Invoke();
        }
    }
}
