using Global;
using Global.Respawn;
using UnityEngine;

namespace Player
{
    public class AutoRespawn : MonoBehaviour
    {
        [SerializeField] private float minHeight = -100;
        [SerializeField] private float maxHeight = 500;

        private Transform _player;
        private RespawnManager _respawnManager;

        private void Start()
        {
            _player = GetComponent<Transform>();
            _respawnManager = GameManager.Instance.GetComponent<RespawnManager>();
        }

        private void Update()
        {
            if (_player.position.y < minHeight ||
                _player.position.y > maxHeight)
            {
                _respawnManager.RespawnPlayer(gameObject);
            }
        }
    }
}