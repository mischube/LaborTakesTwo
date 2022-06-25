using Global;
using Global.Respawn;
using UnityEngine;

public class OnCollisionRespawn : MonoBehaviour
{
    private Transform _player;
    private RespawnManager _respawnManager;
    private bool isPlayerPlant;
    public bool respawnPlants;

    void Start()
    {
        _respawnManager = GameManager.Instance.GetComponent<RespawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = other.transform.GetComponent<Transform>();
        if (respawnPlants)
            _respawnManager.RespawnPlayer(_player.gameObject);
        else
        {
            isPlayerPlant = _player.GetComponent<PhotonPlant>().currentlyAnPlant;
            if (isPlayerPlant)
                return;
            _respawnManager.RespawnPlayer(_player.gameObject);
        }
    }
}
