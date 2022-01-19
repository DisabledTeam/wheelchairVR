using System;
using Interact;
using UnityEngine;
using Valve.VR;

namespace Wheels {
	public class DetachedWheel : MonoBehaviour {
		public DetachInfo detachInfo;

		public void Init(DetachInfo detachInfo) {
			if (this.detachInfo.wheelRoot != null)
			{
				Destroy(this.detachInfo.wheelRoot.gameObject);
			}
			this.detachInfo = detachInfo;
			detachInfo.detachObject = this;
			detachInfo.wheelRoot.transform.SetParent(transform); // блять
			detachInfo.wheelRoot.gameObject.SetActive(false); // вью новый на детаче
		}
	}
}