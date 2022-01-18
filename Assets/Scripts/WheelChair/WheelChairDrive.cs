using System;
using System.Collections.Generic;
using UnityEngine;

namespace WheelChair {
	public class WheelChairDrive : MonoBehaviour {
		[SerializeField] private WheelChairWheelDriver leftWheel;
		[SerializeField] private WheelChairWheelDriver rightWheel;
		[SerializeField] private float lspeed = 10000f;
		[SerializeField] private float rspeed = 10000f;
		private void FixedUpdate() {
			leftWheel.SetLinearVelocity(lspeed);
			rightWheel.SetLinearVelocity(rspeed);
		}
	}
}