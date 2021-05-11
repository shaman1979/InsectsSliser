using MeshSlice;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Slicer.Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "PerfectSlice/LevelInfo", order = 0)]

    public class LevelInfo : SerializedScriptableObject
    {
        public string Name;
        public MeshInfo[] Meshes;
        public ILevelModify[] Modifies; 
    }
}