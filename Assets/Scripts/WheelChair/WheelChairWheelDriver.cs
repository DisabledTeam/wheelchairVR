using System;
using System.Linq;
using Attributes;
using UnityEngine;

namespace WheelChair {
	public class WheelChairWheelDriver : MonoBehaviour {
		[SerializeField] [ReadOnly] private float linearToAngularKoef = 0f;
		private Transform _transform;
		private WheelCollider wheelCollider;

		private void Awake() {
			_transform = transform;
			wheelCollider = GetComponent<WheelCollider>();
		}

		public void SetLinearVelocity(float linearVelocity) {
			wheelCollider.motorTorque = linearVelocity;
		}

	}
}