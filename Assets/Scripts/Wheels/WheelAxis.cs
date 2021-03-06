using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Wheels
{
    public class WheelAxis : MonoBehaviour
    {
        public WheelDetachedEvent WheelDetached = new WheelDetachedEvent();


        [Header("Monitoring")]
        [SerializeField] private WheelSpot leftSpot;

        [SerializeField] private WheelSpot rightSpot;
        [SerializeField] private WheelSpawner wheelSpawner;
        [SerializeField] private DefaultNamespace.WheelChair wheelChair;


        public void Initialize(DefaultNamespace.WheelChair wheelChair, WheelSpawner wheelSpawner)
        {
            this.wheelChair = wheelChair;
            this.wheelSpawner = wheelSpawner;
        }

        public void SpawnWheel(WheelConfig wheelConfig, WheelSpotSite spotSite)
        {
            WheelSpot wheelSpot = GetWheelSpot(spotSite);
            if (!wheelSpot.IsEmpty)
            {
                throw new Exception("Нельзя заспавнить уже есть колесо в этом споте");
            }

            var spawnNewWheel = wheelSpawner.SpawnNewWheel(wheelConfig);
            wheelSpot.SetWheel(spawnNewWheel, wheelConfig);
        }

        public bool CanDetach(WheelSpotSite spotSite)
        {
            return GetWheelSpot(spotSite).CanDetach();
        }

        public DetachInfo Detach(WheelSpotSite spotSite, bool onReplace)
        {
            if (!CanDetach(spotSite)) throw new Exception("CantDetach " + spotSite);

            WheelSpot wheelSpot = GetWheelSpot(spotSite);

            var detachInfo = Detach(wheelSpot);

            if (!onReplace)
            {
                var respawnConfig = wheelChair.DefaultWheel;
                if (detachInfo.wheelConfig.IsInfinite) respawnConfig = detachInfo.wheelConfig;
                SpawnWheel(respawnConfig,spotSite);
            }
            
            WheelDetached.Invoke(new WheelDetachedEventArgs(detachInfo, spotSite, onReplace));
            
            return detachInfo;
        }

        public void Attach(DetachedWheel detachedWheel, WheelSpotSite spotSite)
        {
            Debug.Log($"detachedWheel.detachInfo.wheelConfig: {detachedWheel.DetachInfo.wheelConfig}");
            GetWheelSpot(spotSite).SetWheel(detachedWheel.DetachInfo.wheelRoot, detachedWheel.DetachInfo.wheelConfig);
        }

        private DetachInfo Detach(WheelSpot spot)
        {
            var detachInfo = spot.DetachWheel();
            Debug.Log($"Detach Info: {detachInfo}");
            Debug.Log($"Detach Info: {detachInfo.wheelConfig}");
            detachInfo.detachObject = detachInfo.wheelConfig.InstantiateDetachObject(detachInfo);
            spot.MoveDropItem(detachInfo.detachObject.gameObject);
            
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
        public bool happanedOnReplace;

        public WheelDetachedEventArgs(DetachInfo detachInfo, WheelSpotSite spotSite, bool happanedOnReplace)
        {
            this.detachInfo = detachInfo;
            this.spotSite = spotSite;
            this.happanedOnReplace = happanedOnReplace;
        }
    }
}