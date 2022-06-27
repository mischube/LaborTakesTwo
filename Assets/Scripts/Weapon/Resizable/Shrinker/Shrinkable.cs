using System;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

namespace Weapon.Resizable.Shrinker
{
    public class Shrinkable : Resizable
    {
        public override void Resize()
        {
            if (resizeFactor >= 1)
            {
                throw new InvalidOperationException("Resize factor must be negative to shrink something");
            }

            RPCCall(nameof(ShrinkPhoton));
        }


        [PunRPC]
        [UsedImplicitly]
        public void ShrinkPhoton()
        {
            Debug.LogFormat("Shrinking {0}", gameObject);
            
            transform.localScale *= resizeFactor;
            
            if (!isPlayer)
                return;

            UpdatePlayerStats();
        }
    }
}
