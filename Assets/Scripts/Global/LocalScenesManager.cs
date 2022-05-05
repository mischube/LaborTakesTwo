using Library.StringEnums;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class LocalScenesManager : MonoBehaviourPun
    {
        public Scenes CurrentScene
        {
            get
            {
                var scene = SceneManager.GetActiveScene();
                return scene.GetEnumValue();
            }
        }


        public void LoadScene(Scenes levelName)
        {
            Debug.LogFormat("Loading scene [{0}]", levelName.GetStringValue());

            PhotonNetwork.LoadLevel(levelName.GetStringValue());
        }
    }
}