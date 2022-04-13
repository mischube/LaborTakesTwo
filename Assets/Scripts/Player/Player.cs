using Cinemachine;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviourPun
    {
        private void Start()
        {
            if (!photonView.IsMine)
            {
                DisableComponents();
                DisableScripts();
            }
            else
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
        }
    }
}