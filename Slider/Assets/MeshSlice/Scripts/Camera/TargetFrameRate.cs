﻿using UnityEngine;

namespace MeshSlice
{
  public class TargetFrameRate : MonoBehaviour
  {
    public int targetFrameRate = 60;

    private void Awake()
    {
      Application.targetFrameRate = targetFrameRate;
    }
  }
}
