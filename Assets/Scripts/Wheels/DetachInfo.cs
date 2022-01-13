using UnityEngine;

namespace DefaultNamespace
{
    public class DetachInfo
    {
        public WheelRoot wheelRoot;
        public WheelConfig wheelConfig;
        public GameObject detachObject;

        public DetachInfo(WheelRoot wheelRoot, WheelConfig wheelConfig)
        {
            this.wheelRoot = wheelRoot;
            this.wheelConfig = wheelConfig;
        }
    }
}