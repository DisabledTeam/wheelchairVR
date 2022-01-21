using Interactable;
using UnityEngine;
using UnityEngine.SceneManagement;
using WheelInput;

namespace DefaultNamespace
{
    public class HandLobbyTeleporter : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private HandItem handItem;
        [SerializeField] private SceneNames sceneNames;

        [Header("Monitoring")]
        [SerializeField] private bool isActive;
        [SerializeField] private HandInputProvider handInputProvider;


        private void OnEnable()
        {
            handItem.takenInHand.AddListener(OnTaken);
            handItem.droppedFromHand.AddListener(OnDropped);
        }

        private void OnDropped(HandInputProvider arg0)
        {
            isActive = false;
        }

        private void OnTaken(HandInputProvider arg0)
        {
            handInputProvider = arg0;
            isActive = true;
        }

        private void OnDisable()
        {
            handItem.takenInHand.RemoveListener(OnTaken);
            handItem.droppedFromHand.RemoveListener(OnDropped);
        }

        private void Update()
        {
            if (isActive)
            {
                if (handInputProvider.firstButton)
                {
                    GotoLobby();
                }
            }
        }

        private void GotoLobby()
        {
            var sceneName = sceneNames.GetSceneName(Scenes.ManagerScene);
            SceneManager.LoadScene(sceneName);
        }
    }
}