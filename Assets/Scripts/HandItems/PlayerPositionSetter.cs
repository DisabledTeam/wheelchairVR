using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPositionSetter : MonoBehaviour
    {
        [SerializeField] private bool inChair;

        [SerializeField] private Transform moveTransform;
        [SerializeField] private Transform lookTransform;
        [SerializeField] private PlayerHeight playerHeight;


        public Vector3 Position => moveTransform.position;
        public Vector3 LocalPosition => moveTransform.localPosition;
        public Transform PlayerTransform => moveTransform;

        private void Start()
        {
            var moveTransformPosition = moveTransform.localPosition;

            if (inChair)
                moveTransformPosition.y = playerHeight.YInChair;
            else
                moveTransformPosition.y = playerHeight.YStand;


            moveTransform.localPosition = moveTransformPosition;
        }

        public void MoveTransform(Vector3 moveVector)
        {
            moveTransform.position += moveVector;
        }

        public void MoveLocalTransform(Vector3 moveVector)
        {
            moveTransform.localPosition += moveVector;
        }

        public void MoveRelativeForward(Vector3 moveVector)
        {
            var forward = lookTransform.forward;
            forward.y = 0;

            var rotation = Quaternion.LookRotation(forward, Vector3.up);
            var realMoveVector = rotation * moveVector;
            MoveLocalTransform(realMoveVector);
        }

        public void SetY(float positionY)
        {
            if (inChair)
                playerHeight.YInChair = positionY;
            else
                playerHeight.YStand = positionY;
        }
    }
}