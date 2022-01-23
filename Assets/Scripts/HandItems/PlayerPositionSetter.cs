using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform moveTransform;

        public Vector3 Position => moveTransform.position;
        public Vector3 LocalPosition => moveTransform.localPosition;
        public Transform PlayerTransform => moveTransform;
        
        public void MoveTransform(Vector3 moveVector)
        {
            moveTransform.position += moveVector;
        }
        
        public void MoveLocalTransform(Vector3 moveVector)
        {
            moveTransform.localPosition += moveVector;
        }
        
    }
}