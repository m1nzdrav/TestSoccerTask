using UnityEngine;

namespace UI
{
    public class MenuSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject panelLobby, panelStart;

        public void ChangeToStart()
        {
            panelStart.SetActive(true);
            panelLobby.SetActive(false);
        }

        public void ChangeToLobby()
        {
            panelStart.SetActive(false);
            panelLobby.SetActive(true);
        }

        public void HideAll()
        {
            panelStart.SetActive(false);
            panelLobby.SetActive(false);
        }
    }
}