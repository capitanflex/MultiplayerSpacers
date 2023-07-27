using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minZ;
        [SerializeField] private float maxZ;
        [SerializeField] private int startCoinsCount;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject coinsPrefab;
        [SerializeField] private Sprite[] playerSprites;

        public static GameManager Manager;
    
        private UIManager _uiManager;


        private void Awake()
        {
            Manager = this;
        }

        private void Start()
        {
            _uiManager = UIManager.Manager;
            SpawnPlayer();
            StartCoins(startCoinsCount);
        }

        public void DeleteCoin(GameObject coin)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(coin);
            }
            NewCoin();
        }

        public void PlayerDied()
        {
            photonView.RPC("CheckGameEnd", RpcTarget.AllBuffered);
        }
    
        private void SpawnPlayer()
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minZ, maxZ));
            var player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, new Quaternion(0, 90, 0, 0));
            player.GetComponent<SpriteRenderer>().sprite = playerSprites[PhotonNetwork.LocalPlayer.ActorNumber];
        }
    
        private void StartCoins(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnCoin();
            }
        }

        private async void NewCoin()
        {
            await Task.Delay(3000);
            SpawnCoin();
        }

        private void SpawnCoin()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minZ, maxZ));
                PhotonNetwork.Instantiate(coinsPrefab.name, randomPosition, new Quaternion(0, 0, 0, 0));
            }
        }

    

        [PunRPC]
        private void CheckGameEnd()
        {
            var players = FindObjectsOfType<Player.Player>();
            print(players.Length);
            if (players.Length <= 1)
            {
                _uiManager.ShowPopupWindow(players[0]);
            }
        
        }
    }
}