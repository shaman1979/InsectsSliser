using UnityEngine;

using LightDev;
using Zenject;
using System;
using Assets.Scripts.Tools;

namespace Slicer.Game
{
    public class GameRestarter : IInitializable, IDisposable
    {
        private readonly AsyncHelper asyncHelper;
        private bool isGameStarted;

        public GameRestarter(AsyncHelper asyncHelper)
        {
            this.asyncHelper = asyncHelper;
        }

        public void Initialize()
        {
            Events.SceneLoaded += Reset;
            Events.PointerUp += OnPointerUp;
            Events.RequestFinish += OnRequestFinish;
            Events.RequestReset += Reset;

            asyncHelper.Run(() => Events.RequestReset.Call(), 0.5f);
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
