using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [Serializable]
    public class HealthBar : MonoBehaviour
    {


        /// <summary>
        /// Main bar image 
        /// </summary>
        [Header("Main bar image")]
        [SerializeField]
        private Image foregroundImage;
    
    
    
        /// <summary>
        ///  Time to change bar
        /// </summary>
        [Header("Time to change bar")]
        [SerializeField]
        private float updateSpeedSeconds;
    
    
    
        private Vector3 _distanceFromParent;
        private Transform _myTransform;
        private Transform _cameraTransform;
        private Transform _transformParent;
    
 
        private void Awake()
        {
            _myTransform = transform; //cache the transform
            _transformParent = _myTransform.parent;
            if (Camera.main != null) _cameraTransform = Camera.main.transform; //cace the transform of the camera
        
            // distance from parent needs to keep static relative position of bar to parent 
            _distanceFromParent =  _myTransform.position- _myTransform.parent.position ;
        }

    
    
        /// <summary>
        ///  Change bar value to pct (from 1.0 to 0.0)
        /// </summary>
        /// <param name="pct"> percentage of bar (from 1.0 to 0.0)</param>
        public void HandleHealthChanged(float pct)
        {
            StartCoroutine(ChangeToPct(pct));
        }
    
    

        private IEnumerator ChangeToPct(float pct)
        {
            var preChangePct = foregroundImage.fillAmount;
            var elapsed = 0f;

            while (elapsed<updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
                yield return null;
            }


            foregroundImage.fillAmount = pct;
        
        }


        private void Update()
        {
        
            _myTransform.position =_transformParent.position+_distanceFromParent;
        }
    
        private void LateUpdate()
        {
            var rotation = _cameraTransform.rotation;
            _myTransform.rotation =new Quaternion(rotation.x, 0, 0, rotation.w);
        }
    
    }
}
