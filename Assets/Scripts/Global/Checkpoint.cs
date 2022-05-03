using Global.Respawn;
using Player;
using UnityEngine;

namespace Global
{
    public class Checkpoint : MonoBehaviour
    {
        public RespawnPoint point;


        private void Start()
        {
            point = new RespawnPoint
            {
                position = transform.position,
                scene = GameManager.Instance.CurrentScene
            };
        }


        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<AutoRespawn>().currentCheckpoint = point;
        }
    }
}