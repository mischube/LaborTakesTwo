using Library.StringEnums;
using Photon.Pun;
using UnityEngine;

namespace Global
{
    public class LocalScenesManager : MonoBehaviourPun
    {
        public void LoadScene(Scenes levelName)
        {
            Debug.LogFormat("Loading scene [{0}]", levelName.GetStringValue());

            if (!PhotonNetwork.IsMasterClient)
                return;

            PhotonNetwork.LoadLevel(levelName.GetStringValue());
        }
    }
}
