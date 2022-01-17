using System;
using System.Collections.Generic;
using UnityEngine;
using WheelInput;

namespace Interact {
	public class Interactor : MonoBehaviour {
		public Mutex mutex;
		public MutexHolder holder;

		// [SerializeField] private 
		[SerializeReference] private InputProvider inputProvider;
		private List<Interactable> clickInteractables = new List<Interactable>();
		
		private void OnEnable() {
			// inputProvider.rightHand.firstButton
		}

		private bool TryInteract(Interactable withInteractable) {
			var interactorCondition = 
				!withInteractable.interactorNeedToBeFreeToInteract || // Должна ли рука быть свободной?  
				!mutex.busy;  // Свободна ли рука?
			
			var interactableCondition = 
				!withInteractable.interactableNeedToBeFreeToInteract || // Должен ли предметь быть свободным?  
				!withInteractable.mutex.busy;  // Свободен ли предмет?

			if (!withInteractable.interactorNeedToBeClickedToInteract) {
				// Клик не требуется для взаимодействия
				if (!interactorCondition || !interactableCondition) return false;
				withInteractable.OnInteract(this);
			} else {
				// Нужен клик - отложим до лучших времен
				clickInteractables.Add(withInteractable);
			}

			return true;

		}

		private void OnTriggerEnter(Collider other) {
			if (!other.TryGetComponent<Interactable>(out var interactable)) return;
			interactable.OnTouch(this);
			TryInteract(interactable);
		}

		private void OnTriggerExit(Collider other) {
			if (!other.TryGetComponent<Interactable>(out var interactable)) return;
			interactable.OnTouchEnd(this);
			clickInteractables.Remove(interactable);
		}

		private void Update() {
			
		}
	}
}