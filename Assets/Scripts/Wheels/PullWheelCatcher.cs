using System;
using Attributes;
using DefaultNamespace;
using Interact;
using Interactable;
using UnityEngine;
using WheelInput;

namespace Wheels
{
    public class PullWheelCatcher : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private WheelSpotSite wheelSpotSite;
        [SerializeField] private WheelAxis wheelAxis;
        [SerializeField] private HandsItemControllerManagerSystem handsItemControllerManagerSystem;
        [SerializeField] private Interact.Interactable interactable;


        [Header("Monitoring")]
        [ReadOnly]
        [SerializeField] private Interactor _currentInteractor;

        private void OnEnable()
        {
            interactable.InteractableTouched.AddListener(OnTouchBegin);
            interactable.InteractableTouchEnded.AddListener(OnTouchEnd);
        }

        private void Update()
        {
            if (!_currentInteractor) return;
            if (_currentInteractor.handInputProvider.secondButton == false) return;
            if (!handsItemControllerManagerSystem.IsHandEmpty(_currentInteractor.PlayerHandAxis)) return;
            if (!wheelAxis.CanDetach(wheelSpotSite)) return;

            var detachInfo = wheelAxis.Detach(wheelSpotSite, false);
            var pickUpHandItem = detachInfo.detachObject.GetComponent<PickUpHandItem>();
            if (pickUpHandItem == null) throw new Exception("На оторованном колесе нет PickUpHandItem");
            pickUpHandItem.PickUp(_currentInteractor.PlayerHandAxis, _currentInteractor.handInputProvider);
        }


        private void OnDisable()
        {
            interactable.InteractableTouched.RemoveListener(OnTouchBegin);
            interactable.InteractableTouchEnded.RemoveListener(OnTouchEnd);
        }

        private void OnTouchBegin(InteractableTouchedEventArgs arg0)
        {
            _currentInteractor = arg0.interactor;
        }

        private void OnTouchEnd(InteractableTouchEndedEventArgs arg0)
        {
            if (arg0.interactor == _currentInteractor)
            {
                _currentInteractor = null;
            }
        }
    }
}