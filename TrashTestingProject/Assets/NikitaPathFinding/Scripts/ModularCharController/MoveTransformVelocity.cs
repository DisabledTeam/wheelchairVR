using UnityEngine;

namespace NikitaPathFinding.Scripts.ModularCharController
{
    public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity
    {
        [SerializeField] private float _moveSpeed;
        private Vector3 _velocityVector;

        public void SetVelocity(Vector3 velocityVector)
        {
            this._velocityVector = velocityVector;
        }

        private void Update()
        {
            transform.position += _velocityVector * _moveSpeed * Time.deltaTime;
        }

    }
}
