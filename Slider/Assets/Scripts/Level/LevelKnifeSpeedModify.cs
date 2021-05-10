using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Levels
{
    public class LevelKnifeSpeedModify : LevelModifyDecorator
    {
        public LevelKnifeSpeedModify(ILevelModify decorate) : base(decorate)
        {
        }

        protected override void Modifycation()
        {
            Debug.Log("Ускорение ножа");
        }
    }
}