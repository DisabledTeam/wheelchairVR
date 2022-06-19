using System.Collections.Generic;
using UnityEngine;

//Мой скрипт
namespace NikitaPathFinding.Scripts.ModularCharController
{
    public class MovePositionPathfinding : MonoBehaviour, IMovePosition
    {
        private List<Vector3> _pathVectorList;
        private int _pathIndex = -1;

        public void SetMovePosition(Vector3 movePosition)
        {
            _pathVectorList = GridPathfinding.GridPathfinding.instance.GetPathRouteWithShortcuts(transform.position, movePosition).pathVectorList;
            if (_pathVectorList.Count > 0)
            {
                // Remove first position so he doesn't go backwards
                _pathVectorList.RemoveAt(0);
            }
            if (_pathVectorList.Count > 0)
            {
                _pathIndex = 0;
            }
            else
            {
                _pathIndex = -1;
            }
        }

        private void Update()
        {
            if (_pathIndex != -1)
            {
                // Move to next path position
                Vector3 nextPathPosition = _pathVectorList[_pathIndex];
                Vector3 moveVelocity = (nextPathPosition - transform.position).normalized;
                GetComponent<IMoveVelocity>().SetVelocity(moveVelocity);

                float reachedPathPositionDistance = 1f;
                if (Vector3.Distance(transform.position, nextPathPosition) < reachedPathPositionDistance)
                {
                    _pathIndex++;
                    if (_pathIndex >= _pathVectorList.Count)
                    {
                        // End of path
                        _pathIndex = -1;
                    }
                }
            }
            else
            {
                // Idle
                GetComponent<IMoveVelocity>().SetVelocity(Vector3.zero);
            }
        }
    }
}
