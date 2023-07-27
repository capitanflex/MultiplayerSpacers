using Managers;
using UnityEngine;

namespace Items
{
    public class Coin : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Manager;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _gameManager.DeleteCoin(gameObject);
            }
        }
    }
}
