using System;
using Interact;
using Interactable;
using UnityEngine;
using WheelInput;

namespace DefaultNamespace.Interactable
{
    public class RayCastInteractor : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        [Header("Links")]
        [SerializeField] private Interactor interactor;
        [SerializeField] private float maxRaycast;
        [SerializeField] private LineRenderer lineRenderer;

        [Header("Monitoring")]
        [SerializeField] private Interact.Interactable currentInteractable;


        public bool NeedRaycastInteracting => !interactor.GetLock() &&
                                              interactor.HandInputProvider.joyStick.y > 0.2f &&
                                              interactor.HandInputProvider.joystickTouch &&
                                              interactor.HandInputProvider.secondButton;

        private void Start()
        {
            lineRenderer.enabled = false;
        }

        private void Update()
        {
            Interact.Interactable pickedInteractable = null;
            lineRenderer.enabled = NeedRaycastInteracting;
            // Debug.Log("NeedRaycastInteracting"+NeedRaycastInteracting);
            // Debug.Log("interactor.HandInputProvider.joyStick.y"+interactor.HandInputProvider.joyStick.y);
            // Debug.Log("interactor.HandInputProvider.joystickTouch"+interactor.HandInputProvider.joystickTouch);
            // Debug.Log("interactor.HandInputProvider.secondButton"+interactor.HandInputProvider.secondButton);
            // Debug.Log("!interactor.GetLock()"+!interactor.GetLock());

            if (NeedRaycastInteracting) // Тач + вверх трек
            {
                var ray = new Ray(transform.position, transform.forward);
                UpdateLineReneder();
                if (Physics.Raycast(ray, out var hit, maxRaycast, _layerMask)) // Произошел рейкаст
                {
                    if (hit.collider.gameObject.TryGetComponent<PickUpHandItem>(out var item)) // Есть пикап
                    {
                        var interactable = item.GetComponent<Interact.Interactable>(); // Взяли интерактбл
                        pickedInteractable = interactable;
                    }
                }
            }
            else
            {
                if (currentInteractable) RemoveInteractable(currentInteractable);
            }


            if (pickedInteractable != null && currentInteractable != pickedInteractable)
            {
                SetNewInteractable(pickedInteractable);
            }
            else if (pickedInteractable == null)
            {
                if (currentInteractable) RemoveInteractable(currentInteractable);
            }
        }

        private void SetNewInteractable(Interact.Interactable interactable)
        {
            if (currentInteractable)
            {
                currentInteractable.InteractableInteracted.RemoveListener(OnInteracted);
                RemoveInteractable(currentInteractable);
            }

            currentInteractable = interactable;
            currentInteractable.InteractableInteracted.AddListener(OnInteracted);
            interactor.AddClickInteractables(currentInteractable);
        }

        private void OnInteracted(InteractableInteractedEventArgs arg0)
        {
        }

        private void RemoveInteractable(Interact.Interactable interactable)
        {
            interactor.RemoveClickInteractables(interactable);
            currentInteractable = null;
        }

        private void UpdateLineReneder()
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxRaycast);
        }
    }
}