using UnityEngine;

namespace NikitaPathFinding.Scripts.ModularCharController
{
    public class MoveVelocity : MonoBehaviour, IMoveVelocity
    {
        [SerializeField] private float _movementSpeed;
        public float currentMovementSpeed;

        private Vector3 _velocityVector;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            currentMovementSpeed = _movementSpeed;
        }

        public void SetVelocity(Vector3 velocityVector)
        {
            this._velocityVector = velocityVector;
        }

        private void FixedUpdate()
        {
            _rb.velocity = _velocityVector*_movementSpeed;
        }
    }
}
