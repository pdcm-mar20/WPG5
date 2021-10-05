using UnityEngine;

namespace Lobby
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private NetworkManagerLobby networkManager = null;
        [Header("UI")] [SerializeField] private GameObject landingPage;

        public void HostLobby()
        {
            networkManager.StartHost();
            landingPage.SetActive(false);
        }
    }
}