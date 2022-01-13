using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class WheelAxis : MonoBehaviour
    {
        public WheelDetachedEvent WheelDetached = new WheelDetachedEvent();


        [Header("Monitoring")]
        [SerializeField] private WheelSpot leftSpot;
        [SerializeField] private WheelSpot rightSpot;
        [SerializeField] private WheelSpawner wheelSpawner;
        [SerializeField] private WheelChair wheelChair;


        public void Initialize(WheelChair wheelChair, WheelSpawner wheelSpawner)
        {
            this.wheelChair = wheelChair;
            this.wheelSpawner = wheelSpawner;
        }

        public void RespawnWheel(WheelConfig wheelConfig, WheelSpotSite spotSite)
        {
            WheelSpot wheelSpot = GetWheelSpot(spotSite);
            if (!wheelSpot.IsEmpty)
            {
                var detachInfo = Detach(spotSite);
                wheelSpot.DropItem(detachInfo.detachObject);
            }

            var spawnNewWheel = wheelSpawner.SpawnNewWheel(wheelConfig);
            wheelSpot.SetWheel(spawnNewWheel, wheelConfig);
        }

        public bool CanDetach(WheelSpotSite spotSite)
        {
            return GetWheelSpot(spotSite).CanDetach();
        }

        public DetachInfo Detach(WheelSpotSite spotSite)
        {
            if (!CanDetach(spotSite)) throw new Exception("CantDetach " + spotSite);

            WheelSpot wheelSpot = GetWheelSpot(spotSite);

            var detachInfo = Detach(wheelSpot);
            WheelDetached.Invoke(new WheelDetachedEventArgs(detachInfo, spotSite));
            return detachInfo;
        }

        private DetachInfo Detach(WheelSpot spot)
        {
            var detachInfo = spot.DetachWheel();
            detachInfo.detachObject = detachInfo.wheelConfig.InstantiateDetachObject();

            return detachInfo;
        }

        private WheelSpot GetWheelSpot(WheelSpotSite spotSite)
        {
            return spotSite switch
            {
                WheelSpotSite.Left => leftSpot,
                WheelSpotSite.Right => rightSpot,
                _ => throw new ArgumentOutOfRangeException(nameof(spotSite), spotSite, null)
            };
        }
    }


    [Serializable]
    public class WheelDetachedEvent : UnityEvent<WheelDetachedEventArgs>
    {
    }

    [Serializable]
    public class WheelDetachedEventArgs
    {
        public DetachInfo detachInfo;
        public WheelSpotSite spotSite;

        public WheelDetachedEventArgs(DetachInfo detachInfo, WheelSpotSite spotSite)
        {
            this.detachInfo = detachInfo;
            this.spotSite = spotSite;
        }
    }
}