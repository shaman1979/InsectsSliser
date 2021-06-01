using System.Collections;
using System.Collections.Generic;
using Slicer.EventAgregators;
using UnityEngine;

namespace Slicer.Levels
{
    public interface ILevelModify
    {
        void Apply(IEventsAgregator eventAgregator);
    }
}