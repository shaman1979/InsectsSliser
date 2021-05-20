﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Tools
{
    public static class IntExtensions
    {
        public static bool IsPositive(this int value)
        {
            return value >= 0;
        }

        public static bool IsNegative(this int value)
        {
            return value < 0;
        }
    }
}

