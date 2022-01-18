using System;
using UnityEngine;

public class WheelsTest : MonoBehaviour
{
    [SerializeField] private WheelCollider wheelCollider;
    [SerializeField] private Rigidbody rigidbody=null;
    [SerializeField] private float motorValue;
    [SerializeField] private float brakeValue;

    public void Motor()
    {
        wheelCollider.motorTorque = motorValue;
    }

    public void Brake()
    {
        wheelCollider.brakeTorque = brakeValue;
    }
}