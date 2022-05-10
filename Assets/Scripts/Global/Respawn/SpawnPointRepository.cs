using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Respawn
{
    public class SpawnPointRepository : MonoBehaviour
    {
        [SerializeField] private List<RespawnPoint> spawnPoints = new List<RespawnPoint>();


        public RespawnPoint GetSpawnPoint(Scenes scene)
        {
            return spawnPoints.SingleOrDefault(point => point.scene == scene);
        }

        private void Start()
        {
            //Check if checkpoints list is valid
            var grouping = spawnPoints.GroupBy(point => point.scene);

            foreach (var obj in grouping)
            {
                if (obj.Count() == 1)
                {
                    continue;
                }

                throw new Exception($"There is more than one spawn point for scene '{obj.Key}'");
            }
        }
    }
}