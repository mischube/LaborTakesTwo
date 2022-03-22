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
                GetComponentInChildren<Camera>().enabled = false;
                GetComponentInChildren<AudioListener>().enabled = false;
                GetComponentInChildren<CinemachineFreeLook>().enabled = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}