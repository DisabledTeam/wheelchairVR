using System;
using Interact;
using UnityEngine;
using Valve.VR;

namespace Wheels {
	public class DetachedWheel : MonoBehaviour {
		public DetachInfo DetachInfo;

		public void Init(DetachInfo detachInfo) {
			
			if (this.DetachInfo.wheelRoot != null)
			{
				Destroy(this.DetachInfo.wheelRoot.gameObject);
			}
			
			DetachInfo = detachInfo;
			DetachInfo.detachObject = this;
			DetachInfo.wheelRoot.transform.SetParent(transform,false); // блять
			DetachInfo.wheelRoot.gameObject.SetActive(false); // вью новый на детаче
		}
	}
}