using UnityEngine;

namespace NikitaPathFinding.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _panSpeed;
        [SerializeField] private float _panBorderWitdh;
        [SerializeField] private float xPositionLimit;
        [SerializeField] private float yPositionLimit;

        private void Update()
        {
            MoveCameraDirections();

        }

        private void MoveCameraDirections()
        {
            Vector3 pos = transform.position;
            if (Input.mousePosition.y >= Screen.height - _panBorderWitdh)
            {
                pos.y += _panSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.y <= _panBorderWitdh)
            {
                pos.y -= _panSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x >= Screen.width - _panBorderWitdh)
            {
                pos.x += _panSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x <= _panBorderWitdh)
            {
                pos.x -= _panSpeed * Time.deltaTime;
            }



            transform.position = pos;
        }
    }
}
