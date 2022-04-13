using UnityEngine;

namespace Player
{
    public class AutoRespawn : MonoBehaviour
    {
        public float minHeight = -100;
        public float maxHeight = 500;

        public Vector3 respawnPoint;

        private Transform _player;

        private void Start()
        {
            _player = GetComponent<Transform>();
        }

        private void Update()
        {
            if (_player.position.y < minHeight || _player.position.y > maxHeight)
            {
                _player.position = respawnPoint;
            }
        }
    }
}