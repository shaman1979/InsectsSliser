using Sirenix.OdinInspector;
using UnityEngine;

namespace Slicer.Levels
{
    [CreateAssetMenu(fileName = "Levels", menuName = "PerfectSlice/Levels", order = 1)]
    public class LevelsSettings : SerializedScriptableObject
    {
        public LevelInfo[] levels;
    }
}
