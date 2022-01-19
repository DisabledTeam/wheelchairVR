using System;
using UnityEngine;
using Wheels;

namespace DefaultNamespace
{
    public class WheelSpot : MonoBehaviour
    {
        [SerializeField] private Transform dropItemsPoint;
        [SerializeField] private Transform wheelParentPoint;

        [Header("Monitoring")]
        [SerializeField] private WheelConfig wheelConfig;
        [SerializeField] private WheelRoot wheelRoot;
        [SerializeField] private bool isEmpty;

        public bool IsEmpty => isEmpty;

        public void MoveDropItem(GameObject drop)
        {
            drop.transform.position = dropItemsPoint.position;
        }

        public void SetWheel(WheelRoot wheelRoot, WheelConfig wheelConfig)
        {
            if (this.wheelRoot != null) throw new Exception("Чел тут уже есть колесо");

            wheelRoot.transform.SetParent(wheelParentPoint, false);
            this.wheelRoot = wheelRoot;
            this.wheelConfig = wheelConfig;
            isEmpty = false;
        }

        public DetachInfo DetachWheel()
        {
            var detachedWheel = wheelRoot;
            var detachedConfig = wheelConfig;
            wheelConfig = null;
            wheelRoot = null;
            detachedWheel.OnDetach();
            isEmpty = true;
            return new DetachInfo(detachedWheel, detachedConfig);
        }

        public bool CanDetach()
        {
            return wheelRoot != null;
        }
    }
}