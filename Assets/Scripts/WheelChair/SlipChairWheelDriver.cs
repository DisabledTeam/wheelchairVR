using System;
using Attributes;
using TMPro;
using UnityEngine;

namespace WheelChair {
	public class SlipChairWheelDriver : MonoBehaviour {
		[SerializeField] private ChairWheelDriverInteractable wheelInteractable;
		[SerializeField] private float damping = 0.05f;
		[SerializeField] private float speedSmoothness = 0.05f;
		public TextMeshProUGUI speedometer;
			 
		[field: SerializeField] // ыаоыоаоыоаыоаыо
		public float speed { get; set; }

		private void Update() {
			if(wheelInteractable.Active && wheelInteractable.Triggered)
				speed = Mathf.Lerp(speed, wheelInteractable.rawLinearSpeed, speedSmoothness);
			speedometer.text = speed.ToString("F2");
		}

		private void FixedUpdate() {
			if (Mathf.Abs(speed) > 0.001f)
				speed *= 1 - (damping * Time.fixedDeltaTime);
			else
				speed = 0;
		}
	}
}