using UnityEngine;

namespace DefaultNamespace
{
    public abstract class WheelCombination : ScriptableObject
    {
        public WheelConfig config1;
        public WheelConfig config2;


        public abstract WheelCombinationWorker Init(WheelRoot wheelRoot1, WheelRoot wheelRoot2);


        public bool CanSwitch(WheelConfig wheelConfig1, WheelConfig wheelConfig2)
        {
            return IsOurConfig(wheelConfig1) && IsOurConfig(wheelConfig2);
            bool IsOurConfig(WheelConfig wheelConfig) => wheelConfig == config1 || wheelConfig == config2;
        }
    }
}