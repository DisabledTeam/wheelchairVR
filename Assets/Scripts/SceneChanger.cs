using System;
using HealthBar;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public enum Scenes
    {
        ManagerScene,
        GunScene,
        WheelsMovementScene,
        WheelsShootScene,
        WheelsShootNightScene,
        WheelsSwapDemonstrationScene,
    }


    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private SceneNames sceneNames;
        [SerializeField] private Scenes sceneToLoad;
        [SerializeField] private TextMeshProUGUI sceneLabel;


        private void OnEnable()
        {
            health.dieEvent.AddListener(OnDie);
        }

        private void OnDisable()
        {
            health.dieEvent.RemoveListener(OnDie);
        }

        private void Start()
        {
            sceneLabel.text = sceneToLoad.ToString();
        }

        private void OnDie(Health arg0)
        {
            var sceneName = sceneNames.GetSceneName(sceneToLoad);
            SceneManager.LoadScene(sceneName);
        }
    }
}