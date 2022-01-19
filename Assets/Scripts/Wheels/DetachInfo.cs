using System;
using DefaultNamespace;
using UnityEngine;

namespace Wheels
{
    [Serializable]
    public class DetachInfo
    {
        public WheelRoot wheelRoot;
        public WheelConfig wheelConfig;
        public DetachedWheel detachObject;

        public DetachInfo(WheelRoot wheelRoot, WheelConfig wheelConfig)
        {
            this.wheelRoot = wheelRoot;
            this.wheelConfig = wheelConfig;
        }
    }
}