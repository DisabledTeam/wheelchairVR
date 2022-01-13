using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WheelConfig", menuName = "Wheels/WheelConfig")]
    public class WheelConfig : ScriptableObject
    {
        [Header("Links")]
        public WheelRoot WheelRootPrefab;
        public GameObject DetachPrefab;

        [Header("Settings")]
        public string WheelName;
        public Sprite WheelIcon;
        public bool IsInfinite;


        public WheelRoot InstantiateWheel()
        {
            var wheelRoot = Instantiate(WheelRootPrefab);
            return wheelRoot;
        }

        public GameObject InstantiateDetachObject()
        {
            var newDetach = Instantiate(DetachPrefab);
            return newDetach;
        }
    }
}