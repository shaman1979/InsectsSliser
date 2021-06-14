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
        private SequenceHelper sequenceHelper;

        private void Awake()
        {
            sequenceHelper = new SequenceHelper(transform);
            camera = GetComponent<Camera>();

            ChangeStateInstantly(startState);

            Events.PreReset += OnPreReset;
            Events.GameStart += OnGameStart;
            Events.GameFinish += OnGameFinish;
            Events.SuccessfulSlice += OnSuccessfulCut;
            ShopEvents.ShopShow += OnShopShow;
            ShopEvents.ShopHide += OnShopHide;
        }

        private void OnDestroy()
        {
            Events.PreReset -= OnPreReset;
            Events.GameStart -= OnGameStart;
            Events.GameFinish -= OnGameFinish;
            Events.SuccessfulSlice -= OnSuccessfulCut;
            ShopEvents.ShopShow -= OnShopShow;
            ShopEvents.ShopHide -= OnShopHide;
        }

        private void OnPreReset()
        {
            ChangeState(startState);
        }

        private void OnShopShow()
        {
            ChangeState(shopState);
        }

        private void OnShopHide()
        {
            ChangeState(startState);
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
            sequenceHelper.Sequence(
              camera.DOShakePosition(.4f, .1f, 14, 45)
            );
        }

        private void ChangeStateInstantly(CameraState state)
        {
            sequenceHelper.KillSequences();
            SetLocalPosition(state.position);
            SetLocalRotation(state.rotation);
            camera.fieldOfView = state.fov;
        }

        public void ChangeState(CameraState state)
        {
            sequenceHelper.KillSequences();
            sequenceHelper.Sequence(
                sequenceHelper.MoveLocal(state.position, state.duration).SetEase(state.ease)
            );
            sequenceHelper.Sequence(
                sequenceHelper.RotateLocal(state.rotation, state.duration).SetEase(state.ease)
            );
            sequenceHelper.Sequence(
              camera.DOFieldOfView(state.fov, state.duration).SetEase(state.ease)
            );
        }
    }
}
