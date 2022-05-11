using Cinemachine;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerNetworking : MonoBehaviourPun
    {
        public static GameObject LocalPlayerInstance;


        public static event PlayerLoaded PlayerLoaded;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
                PlayerLoaded?.Invoke();
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (!photonView.IsMine)
            {
                DisableComponents();
                DisableScripts();
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void DisableComponents()
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            GetComponentInChildren<CinemachineFreeLook>().enabled = false;
        }

        private void DisableScripts()
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponentInChildren<PlayerInventory>().enabled = false;
            GetComponentInChildren<Respawn>().enabled = false;
            GetComponentInChildren<PlayerHealth>().enabled = false;
        }
    }
}
