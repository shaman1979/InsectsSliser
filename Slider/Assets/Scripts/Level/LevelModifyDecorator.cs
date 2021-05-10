using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Levels
{
    public abstract class LevelModifyDecorator : ILevelModify
    {
        private readonly ILevelModify decorate;

        public LevelModifyDecorator(ILevelModify decorate)
        {
            this.decorate = decorate;
        }

        public void Apply()
        {
            Apply();
            Modifycation();
        }

        protected abstract void Modifycation();
    }
}