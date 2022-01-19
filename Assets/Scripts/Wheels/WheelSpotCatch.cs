using DefaultNamespace;
using Interactable;
using UnityEngine;

namespace Wheels {
	public class WheelSpotCatch : MonoBehaviour {
		[SerializeField] private HandsItemControllerManagerSystem inventory;
		[SerializeField] private WheelAxis wheelAxis;
		[SerializeField] private WheelSpotSite spotSite;

		private void OnTriggerEnter(Collider other) {
			if (!other.TryGetComponent<DetachedWheelCatch>(out var detachedWheelCatch)) return;
			
			inventory.TryDeEquipHandItem(detachedWheelCatch.pickUp);
			if (wheelAxis.CanDetach(spotSite))
				wheelAxis.Detach(spotSite, true);
			
			wheelAxis.Attach(detachedWheelCatch.detachedWheel, spotSite);
			Destroy(detachedWheelCatch.detachedWheel.gameObject);
		}
	}
}