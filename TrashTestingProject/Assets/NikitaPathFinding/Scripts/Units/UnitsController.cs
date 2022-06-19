using System.Collections.Generic;
using UnityEngine;

namespace NikitaPathFinding.Scripts.Units
{
    public class UnitsController : MonoBehaviour
    {
        [SerializeField] private Transform _selectionAreaTransform;

        private Vector3 _startPosition;
        private List<Unit> _selectedUnitList;
        private Animator _animator;

        private void Awake()
        {
            _selectedUnitList = new List<Unit>();
            _animator = GetComponentInChildren<Animator>();
            _selectionAreaTransform.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //При нажатии  ЛКМ
                _selectionAreaTransform.gameObject.SetActive(true);
                _startPosition = GetMouseWorldPosition();
            }

            if (Input.GetMouseButton(0))
            {
                //Функция при держании ЛКМ
                Vector3 currentMousePosition = GetMouseWorldPosition();

                Vector3 lowerLeft = new Vector3(
                    Mathf.Min(_startPosition.x, currentMousePosition.x),
                    Mathf.Min(_startPosition.y, currentMousePosition.y));
                Vector3 upperRight = new Vector3(
                    Mathf.Max(_startPosition.x, currentMousePosition.x),
                    Mathf.Max(_startPosition.y, currentMousePosition.y));

                _selectionAreaTransform.position = lowerLeft;
                _selectionAreaTransform.localScale = upperRight - lowerLeft;
            }
       
            if (Input.GetMouseButtonUp(0))
            {
                //При поднятии ЛКМ
                _selectionAreaTransform.gameObject.SetActive(false);

                Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPosition, GetMouseWorldPosition());

                //Очистка списка юнитов
                foreach(Unit unit in _selectedUnitList)
                {
                    unit.SetSelectedVisualsVisible(false);
                }
                _selectedUnitList.Clear();

                //Подсчёт всех юнитов в выбранной зоне и добавление в список
                foreach (Collider2D  collider in collider2DArray)
                {
                    Unit unit = collider.GetComponent<Unit>();
                    if (unit!=null)
                    {
                        _selectedUnitList.Add(unit);
                        unit.SetSelectedVisualsVisible(true);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                //При нажатии ПКМ
                Vector3 moveToPosition = GetMouseWorldPosition();

                List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] {1f, 1f, 2f, 3f }, new int[] {1, 5, 10, 15, 20 });

                int targetPositionListIndex = 0;
                foreach (Unit unit in _selectedUnitList)
                {
                    unit.MoveTo(GetMouseWorldPosition());
                    //unit.MoveTo(targetPositionList[targetPositionListIndex]);
                    targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
                }
            }
        }

        private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
        {
            List<Vector3> positionList = new List<Vector3>();
            positionList.Add(startPosition);
            for (int i = 0; i < ringDistanceArray.Length; i++)
            {
                positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
            }
            return positionList;
        }

        private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
        {
            List<Vector3> positionList = new List<Vector3>();
            for (int i = 0; i < positionCount; i++)
            {
                float angle = i * (360f / positionCount);
                Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
                Vector3 position = startPosition + dir * distance;
                positionList.Add(position);
            }

            return positionList;
        }

        private Vector3 ApplyRotationToVector(Vector3 vector, float angle)
        {
            return Quaternion.Euler(0, 0, angle) * vector;
        }


        #region GetMouseWorldPos

        // Get Mouse Position in World with Z = 0f
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }
        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
        #endregion


    }
}
