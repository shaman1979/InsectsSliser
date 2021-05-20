using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Logger
{
    public static class Logger 
    {
        public static bool AssertTry(this bool isFlag, string message)
        {
            if(isFlag)
                Debug.Log(message);
            
            return isFlag;
        }
    }
}