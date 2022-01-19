using System;
using Attributes;
using UnityEngine;

namespace WheelChair {
	public class SlipChairWheelDriver : MonoBehaviour {
		[SerializeField] private ChairWheelDriverInteractable wheelInteractable;
		[SerializeField] private float damping = 0.05f;

		[field: SerializeField] // ыаоыоаоыоаыоаыо
		public float speed { get; set; }

		private void Update() {
			if(wheelInteractable.active)
				speed = wheelInteractable.linearSpeed;
		}

		private void FixedUpdate() {
			if (Mathf.Abs(speed) > 0.001f)
				speed *= 1 - (damping * Time.fixedDeltaTime);
			else
				speed = 0;
		}
	}
}