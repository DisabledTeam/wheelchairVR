using UnityEngine;
using Wheels;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WheelConfig", menuName = "Wheels/WheelConfig")]
    public class WheelConfig : ScriptableObject
    {
        [Header("Links")]
        public WheelRoot WheelRootPrefab;
        public DetachedWheel DetachPrefab;

        [Header("Settings")]
        public string WheelName;
        public Sprite WheelIcon;
        public bool IsInfinite;


        public WheelRoot InstantiateWheel()
        {
            var wheelRoot = Instantiate(WheelRootPrefab);
            return wheelRoot;
        }

        public DetachedWheel InstantiateDetachObject(DetachInfo detachInfo)
        {
            var newDetach = Instantiate(DetachPrefab);
            newDetach.Init(detachInfo);
            return newDetach;
        }
    }
}