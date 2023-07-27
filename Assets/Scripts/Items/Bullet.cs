using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

namespace Items
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float power = 3f;
        [SerializeField] private int damage = 8;
        [SerializeField] private ParticleSystem _particle;
        private PhotonView _view;

        void Start()
        {
            Vector2 localDirection = transform.TransformDirection(Vector2.up);
            GetComponent<Rigidbody2D>().AddForce(localDirection * power, ForceMode2D.Force);
            _view = GetComponent<PhotonView>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player.Player>().GetDamage(damage);
            }

            if (_view.IsMine)
            {
                var particle = PhotonNetwork.Instantiate(_particle.name, transform.position, transform.rotation);
                DeleteParticle(particle);
                PhotonNetwork.Destroy(gameObject);
            }
        }

        private async void DeleteParticle(GameObject particle)
        {
            await Task.Delay(1000);
            PhotonNetwork.Destroy(particle);
        }
    }
}