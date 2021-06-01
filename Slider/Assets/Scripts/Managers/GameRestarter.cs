using UnityEngine;
using LightDev;
using Zenject;
using System;
using Assets.Scripts.Tools;
using Level.Messages;
using Slicer.EventAgregators;
using Slicer.HP.Messages;

namespace Slicer.Game
{
    public class GameRestarter : IInitializable, IDisposable
    {
        private readonly AsyncHelper asyncHelper;
        private readonly IEventsAgregator eventsAgregator;

        private bool isGameStarted;

        private bool isMeshEnd;
        private bool isLevelCalculate;

        public GameRestarter(AsyncHelper asyncHelper, IEventsAgregator eventsAgregator)
        {
            this.asyncHelper = asyncHelper;
            this.eventsAgregator = eventsAgregator;
        }

        public void Initialize()
        {
            Events.SceneLoaded += Reset;
            Events.PointerUp += OnPointerUp;
            Events.RequestFinish += OnRequestFinish;
            Events.RequestReset += Reset;

            eventsAgregator.AddListener<MeshEndMessage>(message => OnMeshEnded());
            eventsAgregator.AddListener<ProgressCalculateEndMessage>(message => OnProgressCalculateEnded());

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
            eventsAgregator.Invoke(new ModifucationActiveMessage());
            Events.GameStart.Call();
        }

        private void OnMeshEnded()
        {
            if (isGameStarted)
                isMeshEnd = true;

            if (isLevelCalculate)
            {
                FinishGame();
            }
        }

        private void OnProgressCalculateEnded()
        {
            if (isGameStarted)
                isLevelCalculate = true;

            if (isMeshEnd)
            {
                FinishGame();
            }
        }


        private void FinishGame()
        {
            isMeshEnd = false;
            isLevelCalculate = false;
            Events.GameFinish.Call();
        }
    }
}