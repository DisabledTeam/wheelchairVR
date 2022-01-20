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
        [SerializeField] private LineRenderer _lineRenderer;

        [Header("Monitoring")]
        [SerializeField] private Interact.Interactable currentInteractable;

        private void Start()
        {
            _lineRenderer.enabled = false;
        }

        private void Update()
        {
            if(currentInteractable) 
                interactor.RemoveClickInteractables(currentInteractable);
            
            if (!(interactor.handInputProvider.joyStick.y > 0.5f) ||
                !interactor.handInputProvider.joystickTouch)
            {
                _lineRenderer.enabled = false;
                return;
            };
            
            var ray = new Ray(transform.position, transform.forward);
            UpdateLineReneder();
            
            if (!Physics.Raycast(ray, out var hit, maxRaycast,_layerMask)) return;
            if (hit.collider.gameObject.TryGetComponent<PickUpHandItem>(out var item))
            {
                var interactable = item.GetComponent<Interact.Interactable>();
                currentInteractable = interactable;
                interactor.AddClickInteractables(currentInteractable);
            }
            else
            {
                interactor.RemoveClickInteractables(currentInteractable);
                currentInteractable = null;
            }
        }


        private void UpdateLineReneder()
        {
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position + transform.forward * maxRaycast);
        }
    }
}