using Cinemachine;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerNetworking : MonoBehaviourPun
    {
        public static GameObject LocalPlayerInstance;

        private WeaponPhoton _weaponObj;


        public static event PlayerLoaded PlayerLoaded;

        
        private void Awake()
        {
            _weaponObj = GetComponentInChildren<WeaponPhoton>();
            
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
                PlayerLoaded?.Invoke();

                _weaponObj.WeaponSwitched += OnWeaponSwitch;
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
            GetComponentInChildren<AutoRespawn>().enabled = false;
            GetComponentInChildren<PlayerHealth>().enabled = false;
        }


        private void OnWeaponSwitch(string weaponName)
        {
            photonView.RPC(nameof(SwitchWeaponPun), RpcTarget.Others, weaponName);
        }

        [PunRPC]
        [UsedImplicitly]
        public void SwitchWeaponPun(string weaponName)
        {
            _weaponObj.ChangeWeapon(weaponName);
        }
    }
}