using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace HealthBar
{
    //TODO Мрак
    [RequireComponent(typeof(TextMesh))]
    public class FloatingDamage : UnityEngine.MonoBehaviour
    {
        private TextMesh _damageLabel;
        public float destroyTime = 3f;
        public float upSpeed = 3;
        public Vector3 spawnPositionRandomizeIntencivity;

        [HideInInspector] public bool needRandomScale;
        [HideInInspector] public float maxSize;
        [HideInInspector] public float minSize;

        private Vector3 _distanceFromParent;
        private Transform _cameraTransform;
        private Transform _transform;
        private float _timer;

        public void SetUpFloatingDamage(float damage, Transform healthParent)
        {
            transform.SetParent(healthParent, true);
            SetUpFloatingDamage(damage);
        }

        public void SetUpFloatingDamage(float damage)
        {
            _damageLabel.text = damage.ToString();
        }

        private void Awake()
        {
            _damageLabel = GetComponent<TextMesh>();
            _cameraTransform = Camera.main.transform;
            _transform = transform;
        }

        private void Start()
        {
            transform.position += new Vector3(
                Random.Range(-spawnPositionRandomizeIntencivity.x, spawnPositionRandomizeIntencivity.x),
                Random.Range(-spawnPositionRandomizeIntencivity.y, spawnPositionRandomizeIntencivity.y),
                Random.Range(-spawnPositionRandomizeIntencivity.z, spawnPositionRandomizeIntencivity.z));

            if (needRandomScale) transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

            StartCoroutine(Animation());
            Destroy(gameObject, destroyTime);
        }


        private IEnumerator Animation()
        {
            var startColor = _damageLabel.color;
            var textColor = _damageLabel.color;
            while (_timer < destroyTime)
            {
                _transform.position += Vector3.up * (upSpeed * Time.deltaTime);
                _timer += Time.deltaTime;
                textColor.a =1- startColor.a * Mathf.Clamp(_timer / destroyTime, 0f, 1f);
                _damageLabel.color = textColor;
                yield return null;
            }
        }
    }
}