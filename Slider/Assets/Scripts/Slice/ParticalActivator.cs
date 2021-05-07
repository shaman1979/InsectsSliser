using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Slice
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticalActivator : MonoBehaviour
    {
        private ParticleSystem particle;

        public void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }

        public void Activate()
        {
            particle.Play();
        }
    }
}