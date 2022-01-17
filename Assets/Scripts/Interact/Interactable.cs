using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interact {

	public class InteractableTouchedEventArgs { }
	public class InteractableTouchedEvent : UnityEvent<InteractableTouchedEventArgs> { }
	
	public class InteractableTouchEndedEventArgs { }
	public class InteractableTouchEndedEvent : UnityEvent<InteractableTouchEndedEventArgs> { }
	
	public class InteractableInteractedEventArgs { }
	public class InteractableInteractedEvent : UnityEvent<InteractableInteractedEventArgs> { }


	public class Interactable : MonoBehaviour {
		[Header("Busy state")]
		public Mutex mutex;
		public MutexHolder holder;

		[Header("Interactable settings")] 
		public bool interactableNeedToBeFreeToInteract;
		public bool interactorNeedToBeFreeToInteract;
		public bool interactorNeedToBeClickedToInteract;
		
		public void OnInteract(Interactor interactor) {
			
		}
		
		public void OnTouch(Interactor interactor) {
			
		}

		public void OnTouchEnd(Interactor interactor) {
			
		}
	}
}