using System;
using Interact;
using UnityEngine;
using Valve.VR;

namespace Wheels {
	public class DetachedWheel : MonoBehaviour {
		public DetachInfo detachInfo;

		public void Init(DetachInfo detachInfo) {
			this.detachInfo = detachInfo;
			detachInfo.wheelRoot.transform.parent = transform; // блять
			detachInfo.wheelRoot.gameObject.SetActive(false); // вью новый на детаче
		}
	}
}