using System;
using JetBrains.Annotations;
using Photon.Pun;

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
            transform.localScale *= resizeFactor;
        }
    }
}
