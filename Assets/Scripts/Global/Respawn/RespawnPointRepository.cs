using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Respawn
{
    public class RespawnPointRepository : MonoBehaviour
    {
        [SerializeField] private List<RespawnPoint> checkPoints = new List<RespawnPoint>();

        public IEnumerable<RespawnPoint> GetRespawnPoints(Scenes scene)
        {
            return checkPoints.Where(point => point.scene == scene).ToList();
        }
    }
}