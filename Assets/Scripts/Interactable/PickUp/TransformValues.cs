using System;
using UnityEngine;

namespace Interactable
{
    [Serializable]
    public class TransformValues
    {
        public Vector3 Position;
        public Quaternion Quaternion;
        public Vector3 Scale;

        public TransformValues(Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            Position = position;
            Quaternion = quaternion;
            Scale = scale;
        }

        public TransformValues(Transform transform)
        {
            Position = transform.position;
            Quaternion = transform.rotation;
            Scale = transform.localScale;
        }

        public void SetValuesToTransform(Transform transform)
        {
            transform.position = Position;
            transform.rotation = Quaternion;
            transform.localScale = Scale;
        }
    }
}