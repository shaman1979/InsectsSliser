using Slicer.Slice;
using UnityEngine;

namespace MeshSlice
{
    [ExecuteAlways]
    public class SlicableObjectMaterial : MonoBehaviour
    {
        public Material material;

        [Header("Points")]
        public Transform firstPoint;
        public Transform secondPoint;

        [SerializeField] private Transform firstRedZone;
        [SerializeField] private Transform secondRedZone;

        [SerializeField]
        private Texture2D texture;

        [SerializeField]
        private MeshGenerator meshGenerator;

        private readonly int firstPointId = Shader.PropertyToID("_Point1");
        private readonly int secondPointId = Shader.PropertyToID("_Point2");
        
        private readonly int firstRedZonePointId = Shader.PropertyToID("_RedZonePoint1");
        private readonly int secondRedZonePointId = Shader.PropertyToID("_RedZonePoint2");
        
        private readonly int mainTextureId = Shader.PropertyToID("_MainTex");

        private void Awake()
        {
            meshGenerator.OnStarted += mesh => texture = mesh.Texture;
        }

        private void Update()
        {
            if (firstPoint && secondPoint && texture)
            {
                material.SetVector(firstPointId, firstPoint.position);
                material.SetVector(secondPointId, secondPoint.position);
                
                material.SetVector(firstRedZonePointId, firstRedZone.position);
                material.SetVector(secondRedZonePointId, secondRedZone.position);
                
                material.SetTexture(mainTextureId, texture);
            }
        }
    }
}
