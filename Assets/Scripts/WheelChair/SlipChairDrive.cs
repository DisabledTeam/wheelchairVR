using System;
using UnityEngine;

namespace WheelChair {
	public class SlipChairDrive : MonoBehaviour {
		[SerializeField] private SlipChairWheelDriver leftWheelDriver;
		[SerializeField] private SlipChairWheelDriver rightWheelDriver;

		private Transform leftWheelTransform;
		private Transform rightWheelTransform;
		
		// ReSharper disable once InconsistentNaming
		private Transform _transform;
		private Rigidbody rb;

		private void Awake() {
			_transform = transform;
			rb = GetComponent<Rigidbody>();

			leftWheelTransform = leftWheelDriver.transform;
			rightWheelTransform = rightWheelDriver.transform;
					
			// local point right between wheels
			rb.centerOfMass = _transform.InverseTransformPoint(Vector3.Lerp(leftWheelTransform.position, rightWheelTransform.position, 0.5f));
		}

		private void Update() {
			rb.velocity = (leftWheelTransform.forward * leftWheelDriver.speed + rightWheelTransform.forward * rightWheelDriver.speed)/2; // linear speed - linear speed = linear speed
			var trueUp = _transform.up;
			var controllableAngularVelocity = (leftWheelDriver.speed - rightWheelDriver.speed) * trueUp; // linear speed to radians per second
			var msk = Vector3.one - trueUp;
			var oldAngularMasked = Vector3.Scale(rb.angularVelocity, msk);
			rb.angularVelocity = oldAngularMasked + controllableAngularVelocity;
			// Debug.Log($"TrueUp: {trueUp}, controllableAngularVelocity: {controllableAngularVelocity}");
		}
		
	}
}