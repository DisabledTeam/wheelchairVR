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

        private Vector3 GetKostylLinearSpeed()
        {
            var linearSpeed = (leftWheelTransform.forward * leftWheelDriver.speed +
                               rightWheelTransform.forward * rightWheelDriver.speed) / 2 *
                              multiplier;
            if (AngularKostylRequired()) return linearSpeed * 2; // Удвоенная скорость вперед, если нет вращения
            return linearSpeed;
        }

        private void Update()
        {
            rb.velocity = GetKostylLinearSpeed(); // linear speed - linear speed = linear speed
            var trueUp = _transform.up;
            var controllableAngularVelocity = GetKostylAngularSpeed() * trueUp; // linear speed to radians per second
            var msk = Vector3.one - trueUp;
            var oldAngularMasked = Vector3.Scale(rb.angularVelocity, msk);
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