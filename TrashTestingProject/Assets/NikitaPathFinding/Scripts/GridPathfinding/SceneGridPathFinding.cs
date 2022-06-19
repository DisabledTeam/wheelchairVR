using System;
using UnityEngine;

namespace NikitaPathFinding.Scripts.GridPathfinding
{
    public class SceneGridPathFinding : MonoBehaviour
    {
        [SerializeField] private Transform worldLowerLeft;
        [SerializeField] private Transform worldUpperRight;
        [SerializeField] private float nodeSize;

        [SerializeReference] private GridPathfinding _grid;

        [ContextMenu("SpawnGrid")]
        public void SpawnGrid()
        {
            _grid = new GridPathfinding(worldLowerLeft.position, worldUpperRight.position, nodeSize);
            _grid.RaycastWalkable();
        }


        private void Awake()
        {
            SpawnGrid();
        }

        private void OnDrawGizmosSelected()
        {
            if (_grid != null)
            {
                var wtfOffset = (worldLowerLeft.position - worldUpperRight.position) / 2;

                for (int x = 0; x < _grid.MapNodes.Length; x++)
                {
                    for (int y = 0; y < _grid.MapNodes[x].Length; y++)
                    {
                        PathNode pathNode = _grid.MapNodes[x][y];

                        var color = pathNode.IsWalkable() ? Color.green : Color.red;
                        Gizmos.color = color;

                        Gizmos.DrawSphere(new Vector3(x * nodeSize, y * nodeSize) + wtfOffset, 0.1f);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (worldLowerLeft == null || worldUpperRight == null) return;

            var bottomLeft = worldLowerLeft.position;
            var topRight = worldUpperRight.position;
            var topLeft = new Vector3(bottomLeft.x, topRight.y);
            var bottomRight = new Vector3(topRight.x, bottomLeft.y);
            
            Gizmos.color = Color.magenta;
            
            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);

            
            Gizmos.DrawSphere(bottomLeft, 0.2f);
            Gizmos.DrawSphere(topRight, 0.2f);


            
        }
    }
}