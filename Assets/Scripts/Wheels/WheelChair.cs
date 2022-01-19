using System;
using System.Collections.Generic;
using UnityEngine;
using Wheels;

namespace DefaultNamespace
{
    public class WheelChair : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private WheelSpawner wheelSpawner;
        [SerializeField] private WheelAxis wheelAxis;
        [SerializeField] private WheelConfig defaultWheel;

        public WheelConfig DefaultWheel => defaultWheel;

        public void Initialize()
        {
            wheelAxis.Initialize(this, wheelSpawner);
        }

        private void OnEnable()
        {
            wheelAxis.WheelDetached.AddListener(OnWheelDetached);
        }

        private void OnDisable()
        {
            wheelAxis.WheelDetached.RemoveListener(OnWheelDetached);
        }

        private void Start()
        {
            Initialize();
        }


        private void OnWheelDetached(WheelDetachedEventArgs arg0)
        {
            if (arg0.happanedOnReplace) return;
            var respawnConfig = defaultWheel;
            if (arg0.detachInfo.wheelConfig.IsInfinite) respawnConfig = arg0.detachInfo.wheelConfig;
            wheelAxis.RespawnWheel(respawnConfig,arg0.spotSite);
        }

        private void CheckSpawnNewWheel()
        {
        }
    }
}