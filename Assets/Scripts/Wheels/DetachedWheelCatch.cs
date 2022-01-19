using Interactable;
using UnityEngine;

namespace Wheels {
	public class DetachedWheelCatch : MonoBehaviour {
		[field:SerializeField]
		public DetachedWheel detachedWheel { get; private set; }
		
		[field:SerializeField]
		public PickUpHandItem pickUp { get; private set; }
	}
}