using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Tools
{
    public static class FloatExtensions
    {
        public static bool IsNegative(this float value)
        {
            return value < 0;
        }

        public static bool IsMore(this float value, float to)
        {
            return value.CompareTo(to).Equals(1);
        }

        public static bool IsZero(this float value)
        {
            return value.Equals(0f);
        }
    }
}