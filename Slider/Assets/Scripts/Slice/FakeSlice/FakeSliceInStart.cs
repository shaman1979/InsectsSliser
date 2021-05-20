using System;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer;
using UnityEngine;

namespace Slicer.Slice.Fake
{
    public class FakeSliceInStart : MonoBehaviour
    {
        [SerializeField] private Mesh mesh;
        [SerializeField] private Material[] materials;
        
        private void Awake()
        {
            var data = new BzMeshData(mesh, materials);
        }
    }
}