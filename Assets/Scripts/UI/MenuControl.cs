using ClientCore;
using ClientCore.Data;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuControl : MonoBehaviour
    {
        [SerializeField] private MenuSwitcher menuSwitcher;
        [SerializeField] private ColorEnum colorEnum = ColorEnum.Red;

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            menuSwitcher.ChangeToLobby();
        }

        public void Join()
        {
            NetworkManager.Singleton.StartClient();
            menuSwitcher.ChangeToLobby();
        }

        public void TakeColor(int color)
        {
            NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerData>().ChangeVariable(color);
        }

        public void Ready()
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}