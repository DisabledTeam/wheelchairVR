using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

namespace WheelChair
{
    public class SlipChairDrive : MonoBehaviour
    {
        [SerializeField] private SlipChairWheelDriver leftWheelDriver;
        [SerializeField] private SlipChairWheelDriver rightWheelDriver;
        [SerializeField] private float multiplier = 0.5f;

        private Transform leftWheelTransform;
        private Transform rightWheelTransform;

        // ReSharper disable once InconsistentNaming
        private Transform _transform;
        private Rigidbody rb;

        private void Awake()
        {
            _transform = transform;
            rb = GetComponent<Rigidbody>();

            leftWheelTransform = leftWheelDriver.transform;
            rightWheelTransform = rightWheelDriver.transform;

            // local point right between wheels
            rb.centerOfMass =
                _transform.InverseTransformPoint(Vector3.Lerp(leftWheelTransform.position, rightWheelTransform.position,
                    0.5f));
        }

        [SerializeField] private float absAngularSpeedEnableThreshold = 0.3f;

        private bool AngularKostylRequired()
        {
            var absThres = Mathf.Abs(absAngularSpeedEnableThreshold);
            return (leftWheelDriver.speed > absThres && rightWheelDriver.speed > absThres ||
                    leftWheelDriver.speed < -absThres && rightWheelDriver.speed < -absThres);
        }

        private float GetKostylAngularSpeed()
        {
            if (AngularKostylRequired()) return 0f; // Не добавлять вращения, если колеса сонаправлены и быстрее порога
            return (leftWheelDriver.speed - rightWheelDriver.speed) / 2f;
        }

        private Vector3 GetKostylLinearSpeed() {
            var absUp = Vector3.up;
            var msk = Vector3.one - absUp;
            
            var linearSpeed = (leftWheelTransform.forward * leftWheelDriver.speed + rightWheelTransform.forward * rightWheelDriver.speed) / 2 * multiplier;
            linearSpeed.Scale(msk);
                
            var upSpeed = rb.velocity;
            Vector3.Scale(upSpeed, absUp);
            
            if (AngularKostylRequired()) return upSpeed + linearSpeed * 2; // Удвоенная скорость вперед, если нет вращения
            return upSpeed + linearSpeed;
        }

        private void Update()
        {
            var trueUp = _transform.up;
            var msk = Vector3.one - trueUp;
            
            rb.velocity = GetKostylLinearSpeed(); // linear speed - linear speed = linear speed
            var controllableAngularVelocity = GetKostylAngularSpeed() * trueUp; // linear speed to radians per second
            var oldAngularMasked = rb.angularVelocity;
            Vector3.Scale(oldAngularMasked, msk);
            rb.angularVelocity = oldAngularMasked + controllableAngularVelocity;
            // Debug.Log($"TrueUp: {trueUp}, controllableAngularVelocity: {controllableAngularVelocity}");
        }

        private void OnDrawGizmos()
        {
            var ang = GetKostylAngularSpeed();
            var tf = transform;
            var position = tf.position;
            Gizmos.DrawLine(position, position + tf.right * GetKostylAngularSpeed());
        }
    }
}