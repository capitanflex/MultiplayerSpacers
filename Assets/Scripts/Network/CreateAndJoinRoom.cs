using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Network
{
    public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField createInput;
        [SerializeField] private TMP_InputField joinInput;
        [SerializeField] private TMP_InputField nickNameInput;
        [SerializeField] private GameObject createAndJoinPanel;
        [SerializeField] private GameObject waitingPanel;
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject [] playerSlots;
    
        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(createInput.text);
        
            startButton.SetActive(true);
            SetName();
            playerSlots[0].GetComponentInChildren<TextMeshProUGUI>().text = nickNameInput.text;
        }

        public void JoinRoom()
        {
            SetName();
            PhotonNetwork.JoinRoom(joinInput.text);
        }

        public void InitStartGame()
        {
            photonView.RPC("StartGame", RpcTarget.All);
        }
    
        public override void OnJoinedRoom()
        {
        
            createAndJoinPanel.SetActive(false);
            waitingPanel.SetActive(true);
        }
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            photonView.RPC("PlayerEntered", RpcTarget.AllBuffered);
        }

        private void SetName()
        {
            if (nickNameInput.text != null)
            {
                PhotonNetwork.LocalPlayer.NickName = nickNameInput.text;
            }
        }
    
        [PunRPC]
        private void StartGame()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    
        [PunRPC]
        public void PlayerEntered()
        {
            for (int slot = 0; slot < ((ICollection)PhotonNetwork.PlayerList).Count; slot++)
            {
                playerSlots[slot].SetActive(true);
                playerSlots[slot].GetComponentInChildren<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[slot].NickName;
            }
        }

    }
}