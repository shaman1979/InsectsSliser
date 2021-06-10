using UnityEngine;

namespace View.Messages
{
    public class MaterialChangeMessage
    {
        public Material CurrentMaterial { get; }

        public MaterialChangeMessage(Material changeMaterial)
        {
            CurrentMaterial = changeMaterial;
        }
    }
}