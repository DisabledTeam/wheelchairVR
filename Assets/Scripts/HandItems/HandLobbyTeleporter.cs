using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class HandLobbyTeleporter : HandItemTool
    {
        [Header("Links")]
        [SerializeField] private SceneNames sceneNames;

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