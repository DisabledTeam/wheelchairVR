using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HandLobbyTeleporter : HandItemTool
    {
        [Header("Settings")]
        [SerializeField] private float holdTime = 1f;

        [Header("Links")]
        [SerializeField] private Slider slider;
        [SerializeField] private SceneNames sceneNames;
        private Coroutine _tpCoroutine;

        protected override void ItemTakenInHand()
        {
            handInputProvider.firstButtonChanged.AddListener(OnFirstButtonChange);
        }

        protected override void ItemDroppedFromHand()
        {
            handInputProvider.firstButtonChanged.RemoveListener(OnFirstButtonChange);
        }

        private void OnFirstButtonChange(bool arg0)
        {
            if (arg0)
            {
                _tpCoroutine = StartCoroutine(TeleportCoroutine());
            }
            else
            {
                if (_tpCoroutine != null)
                {
                    StopCoroutine(_tpCoroutine);
                    _tpCoroutine = null;
                    slider.value = 0;
                }
            }
        }

        private IEnumerator TeleportCoroutine()
        {
            var time = 0f;
            while (time < holdTime)
            {
                time += Time.deltaTime;
                slider.value = time / holdTime;
                yield return null;
            }

            GotoLobby();
        }


        private void GotoLobby()
        {
            var sceneName = sceneNames.GetSceneName(Scenes.ManagerScene);
            SceneManager.LoadScene(sceneName);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if(handInputProvider)   handInputProvider.firstButtonChanged.RemoveListener(OnFirstButtonChange);
        }
    }
}