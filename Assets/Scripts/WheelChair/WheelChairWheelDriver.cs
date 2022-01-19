using System;
using System.Linq;
using Attributes;
using UnityEngine;

namespace WheelChair {
	public class WheelChairWheelDriver : MonoBehaviour {
		[SerializeField] [ReadOnly] private float linearToAngularKoef = 0f;
		private Transform _transform;
		private WheelCollider wheelCollider;
		[SerializeField] private float radius;

		private void Awake() {
			_transform = transform;
			wheelCollider = GetComponent<WheelCollider>();
		}

		public void SetLinearVelocity(float linearVelocity) {
			if(linearVelocity > Mathf.Epsilon)
				wheelCollider.motorTorque = linearVelocity;
		}

		// private void OnDrawGizmos() {
		// 	var tf = transform;
		// 	var origin = tf.position;
		// 	const int rayCount = 18;
		//
		// 	for (var i = 0; i < rayCount; i++) {
		// 		var angle = 2f / rayCount * i * Mathf.PI; // angle in radians
		// 		var vectorByAngle = new Vector3(0, Mathf.Sin(angle), Mathf.Cos(angle));
		// 		if (Physics.Raycast(tf.position, vectorByAngle, radius)) {
		// 			Gizmos.color = Color.green;
		// 		} else Gizmos.color = Color.red;
		//
		// 		Gizmos.DrawLine(origin, origin+transform.TransformVector(vectorByAngle)*radius);
		// 	}
		// }
	}
}