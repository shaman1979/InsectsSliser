using UnityEngine;

namespace Shop
{
    [System.Serializable]
    public abstract class OpenItem
    {
        [field:SerializeField]
        public int OpenValue { get; private set; }
    }
    
    [System.Serializable]
    public class LevelOpenItem : OpenItem
    {
    }

    [System.Serializable]
    public class StarsOpenItem : OpenItem
    {
        
    }
}
