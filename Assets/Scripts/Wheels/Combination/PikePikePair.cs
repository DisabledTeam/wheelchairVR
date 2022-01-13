using UnityEngine;

namespace DefaultNamespace
{
    public class PikePikePair : WheelCombination
    {
        public override WheelCombinationWorker Init(WheelRoot w1, WheelRoot w2)
        {
            WheelCombinationWorker wheelCombinationWorker =
                new PikePikeWorker((Test1WheelRoot) w1, (Test1WheelRoot) w2);
            return wheelCombinationWorker;
        }

        public class PikePikeWorker : WheelCombinationWorker
        {
            public PikePikeWorker(Test1WheelRoot wheel1, Test1WheelRoot wheel2)
            {
            }

            public override void Update()
            {
            }

            public override void Start()
            {
            }

            public override void End()
            {
            }
        }


        public void CombinationUpdate(Test1WheelRoot w1, Test1WheelRoot w2)
        {
            Debug.Log("CombinationUpdate");
        }
    }
}