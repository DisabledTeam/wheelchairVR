using System;
using Interact.Lock;
using UnityEngine;
using UnityEngine.Events;

namespace Interact {
	public class InteractableTouchedEventArgs {
		public Interactor interactor;

		public InteractableTouchedEventArgs(Interactor interactor) {
			this.interactor = interactor;
		}
	}

	public class InteractableTouchedEvent : UnityEvent<InteractableTouchedEventArgs> { }

	public class InteractableTouchEndedEventArgs {
		public Interactor interactor;

		public InteractableTouchEndedEventArgs(Interactor interactor) {
			this.interactor = interactor;
		}
	}

	public class InteractableTouchEndedEvent : UnityEvent<InteractableTouchEndedEventArgs> { }

	public class InteractableInteractedEventArgs {
		public Interactor interactor;

		public InteractableInteractedEventArgs(Interactor interactor) {
			this.interactor = interactor;
		}
	}

	public class InteractableInteractedEvent : UnityEvent<InteractableInteractedEventArgs> { }
	
	public class CanBeInteractedBeginEvent : UnityEvent<InteractableInteractedEventArgs> { }
	public class CanBeInteractedEndEvent : UnityEvent<InteractableInteractedEventArgs> { }


	public class Interactable : LockableMonoBehaviour {
		[Header("Interactable settings")]
		public bool interactableNeedToBeFreeToInteract;

		public bool interactorNeedToBeFreeToInteract;
		public bool interactorNeedToBeClickedToInteract;

		public InteractableTouchedEvent InteractableTouched = new InteractableTouchedEvent();
		public InteractableTouchEndedEvent InteractableTouchEnded = new InteractableTouchEndedEvent();
		public InteractableInteractedEvent InteractableInteracted = new InteractableInteractedEvent();
		public CanBeInteractedBeginEvent CanBeInteractedBegin = new CanBeInteractedBeginEvent();
		public CanBeInteractedEndEvent CanBeInteractedEnd = new CanBeInteractedEndEvent();

		public void OnInteract(Interactor interactor) {
			InteractableInteracted.Invoke(new InteractableInteractedEventArgs(interactor));
		}

		public void OnTouch(Interactor interactor) {
			InteractableTouched.Invoke(new InteractableTouchedEventArgs(interactor));
		}

		public void OnTouchEnd(Interactor interactor) {
			InteractableTouchEnded.Invoke(new InteractableTouchEndedEventArgs(interactor));
		}

		public void OnAddedToCanBeInteracted(Interactor interactor)
		{
			CanBeInteractedBegin.Invoke(new InteractableInteractedEventArgs(interactor));
		}

		public void OnRemovedToCanBeInteracted(Interactor interactor)
		{
			CanBeInteractedEnd.Invoke(new InteractableInteractedEventArgs(interactor));
		}
	}
}