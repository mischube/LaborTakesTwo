using System;
using UnityEngine;

namespace Global.Respawn
{
    [Serializable]
    public struct RespawnPoint
    {
        public Scenes scene;

        public Vector3 position;
    }
}