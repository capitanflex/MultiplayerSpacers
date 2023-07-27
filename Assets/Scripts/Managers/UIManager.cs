using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviourPunCallbacks
    {
        public static UIManager Manager;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private GameObject popupCanvas;
        [SerializeField] private GameObject cameraCanvas;
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private TextMeshProUGUI winCoinsText;
    
        private void Awake()
        {
            Manager = this;
        }

        public void HpChanged(int newValue)
        {
            hpSlider.value = newValue;
        }
    
        public void CoinCountChanged(int newValue)
        {
            coinsText.text = "Coins: " + newValue;
        }

        public void ShowPopupWindow(Player.Player winner)
        {
            popupCanvas.SetActive(true);
            cameraCanvas.SetActive(false);

            winText.text = "Player : " + winner.nickName + Environment.NewLine + "WIN";
            winCoinsText.text = "Collect " + winner.coinsCount + " coins";
        }

        public void ExitToLobby()
        {
            PhotonNetwork.LoadLevel("Lobby");
        }
    }
}
