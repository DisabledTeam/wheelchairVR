using System;
using Attributes;
using Interact;
using UnityEngine;

namespace WheelChair
{
    [RequireComponent(typeof(Interact.Interactable))]
    public class ChairWheelDriverInteractable : MonoBehaviour
    {
        [SerializeField] private Vector3 movementVector = Vector3.forward;

        private Interact.Interactable interactable;

        private Transform _transform;
        private Interactor interactor;

        private Transform handTransform;
        private Vector3 lastHandPosition;

        public float rawLinearSpeed { get; private set; }

        [field: SerializeField, ReadOnly] // ыаоыоаоыоаыоаыо
        public bool Active { get; private set; }
        public bool Triggered => interactor != null && interactor.handInputProvider.firstButton;

        private void Awake()
        {
            _transform = transform;
            interactable = GetComponent<Interact.Interactable>();
        }

        private void OnEnable()
        {
            interactable.InteractableTouched.AddListener(OnHandTouchedInteractable);
            interactable.InteractableTouchEnded.AddListener(OnHandUntouchedInteractable);
        }

        private void OnDisable()
        {
            interactable.InteractableTouched.RemoveListener(OnHandTouchedInteractable);
            interactable.InteractableTouchEnded.RemoveListener(OnHandUntouchedInteractable);
        }

        private void Update()
        {
            if (!Active) return;
            var position = _transform.InverseTransformPoint(handTransform.position);
            if (Triggered) // interactor.handInputProvider.firstButton
            {
                
                var delta = position - lastHandPosition;
                var trueMovementVector = movementVector; // _transform.TransformVector(movementVector).normalized;
                var trueDelta = Vector3.Dot(delta, trueMovementVector);
                rawLinearSpeed = trueDelta / Time.deltaTime;
                Debug.Log(
                    $"delta: {delta}, " +
                    $"trueDelta: {trueDelta}, " +
                    $"linearSpeed: {rawLinearSpeed}");
            }
            lastHandPosition = position;
        }

        private void OnHandTouchedInteractable(InteractableTouchedEventArgs arg0)
        {
            Active = true;
            interactor = arg0.interactor;
            handTransform = interactor.transform;
            lastHandPosition = _transform.InverseTransformPoint(handTransform.position);
        }

        private void OnHandUntouchedInteractable(InteractableTouchEndedEventArgs arg0)
        {
            Active = false;
            interactor = null;
            handTransform = null;
        }

        /* -- */

        private void OnDrawGizmos()
        {
            var tf = transform;
            var pos = tf.position;
            Gizmos.DrawLine(pos, pos + tf.TransformVector(movementVector) * 3);
        }
    }
}