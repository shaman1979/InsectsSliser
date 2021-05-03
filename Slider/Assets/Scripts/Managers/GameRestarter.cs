using UnityEngine;

using LightDev;
using Zenject;
using System;

namespace Slice.Game
{
    public class GameRestarter : IInitializable, IDisposable
    {
        private bool isGameStarted;

        public void Initialize()
        {
            Events.SceneLoaded += Reset;
            Events.PointerUp += OnPointerUp;
            Events.RequestFinish += OnRequestFinish;
            Events.RequestReset += Reset;

            Events.RequestReset.Call();
        }
        public void Dispose()
        {
            Events.SceneLoaded -= Reset;
            Events.PointerUp -= OnPointerUp;
            Events.RequestFinish -= OnRequestFinish;
            Events.RequestReset -= Reset;
        }

        private void OnRequestFinish()
        {
            FinishGame();
        }

        private void OnPointerUp()
        {
            if (!isGameStarted)
            {
                StartGame();
            }
        }

        private void Reset()
        {
            isGameStarted = false;

            Events.PreReset.Call();
            Events.PostReset.Call();
        }

        private void StartGame()
        {
            isGameStarted = true;
            Events.GameStart.Call();
        }

        private void FinishGame()
        {
            Events.GameFinish.Call();
        }
    }
}
