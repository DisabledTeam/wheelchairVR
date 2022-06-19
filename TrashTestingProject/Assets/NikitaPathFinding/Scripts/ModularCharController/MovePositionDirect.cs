using UnityEngine;

namespace NikitaPathFinding.Scripts.ModularCharController
{
    public class MovePositionDirect : MonoBehaviour, IMovePosition
    {
        private Vector3 _movePosition;
        public Vector3 moveDirection;

        void Awake()
        {
            _movePosition = transform.position;
        }

        public void SetMovePosition(Vector3 movePosition)
        {
            this._movePosition = movePosition;
        }

        private void Update()
        {
            moveDirection = (_movePosition - transform.position).normalized;
            if (Vector3.Distance(_movePosition, transform.position) < 1f)
            {
                moveDirection = Vector3.zero;
            }
            GetComponent<IMoveVelocity>().SetVelocity(moveDirection);
        }
    }
}
