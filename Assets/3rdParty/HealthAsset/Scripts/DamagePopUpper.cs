using System;
using UnityEngine;

namespace HealthBar
{
    public class DamagePopUpper : UnityEngine.MonoBehaviour
    {
        public bool needToAttachToTransform = false;
        [SerializeField] private FloatingDamage standartFloatingText;
        [SerializeField] private FloatingDamage criticalFloatingText;
        private Transform _transform;

        public void SpawnFloatingDamage(float damage, bool isCritical)
        {
            var prefabToSpawn = isCritical ? criticalFloatingText : standartFloatingText;

            if (prefabToSpawn == null)
            {
                throw new NullReferenceException("Floating Text Prefab not set");
            }

            var newText = Instantiate(prefabToSpawn);
            newText.transform.position = transform.position;
            if (needToAttachToTransform) newText.SetUpFloatingDamage(damage, _transform);
            else
            {
                newText.SetUpFloatingDamage(damage);
            }
        }


        private void Awake()
        {
            _transform = transform;
        }
    }
}