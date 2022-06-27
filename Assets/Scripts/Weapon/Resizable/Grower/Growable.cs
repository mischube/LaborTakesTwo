using System;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

namespace Weapon.Resizable.Grower
{
    public class Growable : Resizable
    {
        public override void Resize()
        {
            if (resizeFactor <= 1)
            {
                throw new InvalidOperationException("Resize factor must be positive to grow something");
            }

            RPCCall(nameof(GrowPhoton));
        }


        [PunRPC]
        [UsedImplicitly]
        public void GrowPhoton()
        {
            Debug.LogFormat("Growing {0}", gameObject);
            
            transform.localScale *= resizeFactor;

            if (!isPlayer)
                return;

            UpdatePlayerStats();
        }
    }
}
