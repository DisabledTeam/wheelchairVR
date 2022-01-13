using UnityEngine;

namespace DefaultNamespace
{
    public class WheelSpawner : MonoBehaviour
    {
        public WheelRoot SpawnNewWheel(WheelConfig wheelConfig)
        {
            var newWheel = Instantiate(wheelConfig.WheelRootPrefab);
            return newWheel;
        }
    }
}