using System;

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

            ResizeInternal();
        }
    }
}
