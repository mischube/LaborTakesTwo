using System;

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

            ResizeInternal();
        }
    }
}
