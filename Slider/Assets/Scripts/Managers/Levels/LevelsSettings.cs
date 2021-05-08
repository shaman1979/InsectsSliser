using UnityEngine;

namespace Slicer.Levels
{
    [CreateAssetMenu(fileName = "Levels", menuName = "PerfectSlice/Levels", order = 1)]
    public class LevelsSettings : ScriptableObject
    {
        public LevelInfo[] levels;
    }
}
