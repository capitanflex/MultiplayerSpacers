using Items;
using Managers;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public int coinsCount;
        public string nickName;
    
        [SerializeField] public int hp = 100;
        [SerializeField] public float fireRate = 0.2f;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject firePoint;

        private FireButtonPressCheck _fireButton;
        private GameManager _gameManager;
        private UIManager _uiManager;
        private PhotonView _view;
        private Sprite[] _playerSprites;
    
        private float _lastFireTime;


        private void Start()
        {
            _fireButton = FireButtonPressCheck.FireButton;
            _uiManager = UIManager.Manager;
            _gameManager = GameManager.Manager;
            _view = GetComponent<PhotonView>();
            nickName = _view.Owner.NickName;
        }

        void Update()
        {
            if (_view.IsMine && _fireButton.isButtonPressed)
            {
                Fire();
            }
        }

        public void GetDamage(int damage)
        {
            if (_view.IsMine)
            {
                hp -= damage;
                _uiManager.HpChanged(hp);
                if (hp <= 0)
                {
                    PhotonNetwork.Destroy(gameObject);
                    _gameManager.PlayerDied();
                }
            }
        }
    
        private void Fire()
        {
            if (Time.time - _lastFireTime >= fireRate)
            {
                PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.transform.position, transform.rotation);
                _lastFireTime = Time.time;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            
            if (collider.gameObject.CompareTag("Coin"))
            {
                coinsCount++;
                if(_view.IsMine)
                {
                    _uiManager.CoinCountChanged(coinsCount);
                }
                
            }
        }
    
    }
}