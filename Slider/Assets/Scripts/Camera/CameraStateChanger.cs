using UnityEngine;

using LightDev;
using LightDev.Core;

using DG.Tweening;
using Slicer.Shop.Events;

namespace MeshSlice
{
    public class CameraStateChanger : Base
    {
        [SerializeField]
        private CameraState startState;

        [SerializeField]
        private CameraState gameState;

        [SerializeField]
        private CameraState finishState;

        [SerializeField]
        private CameraState shopState;

        private new Camera camera;

        private void Awake()
        {
            camera = GetComponent<Camera>();

            ChangeStateInstantly(startState);

            Events.PreReset += OnPreReset;
            Events.GameStart += OnGameStart;
            Events.GameFinish += OnGameFinish;
            Events.SuccessfulSlice += OnSuccessfulCut;
            ShopEvents.ShopShow += OnShopShow;
        }

        private void OnDestroy()
        {
            Events.PreReset -= OnPreReset;
            Events.GameStart -= OnGameStart;
            Events.GameFinish -= OnGameFinish;
            Events.SuccessfulSlice -= OnSuccessfulCut;
            ShopEvents.ShopShow -= OnShopShow;
        }

        private void OnPreReset()
        {
            ChangeState(startState);
        }

        private void OnShopShow()
        {
            ChangeState(shopState);
        }


        private void OnGameStart()
        {
            ChangeState(gameState);
        }

        private void OnGameFinish()
        {
            ChangeState(finishState);
        }

        private void OnSuccessfulCut(int left, int right)
        {
            Sequence(
              camera.DOShakePosition(.4f, .1f, 14, 45)
            );
        }

        private void ChangeStateInstantly(CameraState state)
        {
            KillSequences();
            SetLocalPosition(state.position);
            SetLocalRotation(state.rotation);
            camera.fieldOfView = state.fov;
        }

        public void ChangeState(CameraState state)
        {
            KillSequences();
            Sequence(
              MoveLocal(state.position, state.duration).SetEase(state.ease)
            );
            Sequence(
              RotateLocal(state.rotation, state.duration).SetEase(state.ease)
            );
            Sequence(
              camera.DOFieldOfView(state.fov, state.duration).SetEase(state.ease)
            );
        }
    }
}
