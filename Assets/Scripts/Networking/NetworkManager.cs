using Photon.Pun;
using UnityEngine;

namespace Networking
{
    public class NetworkManager : MonoBehaviour
    {
        public GameObject playerPrefab;
    
        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(65,16,-43),Quaternion.identity);
        }
    }
}