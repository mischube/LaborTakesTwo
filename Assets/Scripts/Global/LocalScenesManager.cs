using System;
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
                var scene = SceneManager.GetActiveScene().name;
                var success = Enum.TryParse<Scenes>(scene, out var scenesEnumValue);
                return success ? scenesEnumValue : throw new InvalidCastException();
            }
        }


        public void LoadScene(Scenes levelName)
        {
            Debug.LogFormat("Loading scene [{0}]", levelName.GetStringValue());

            PhotonNetwork.LoadLevel(levelName.GetStringValue());
        }
    }
}