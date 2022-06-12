namespace Weapon.Resizable.Shrinker
{
    public class ShrinkerWeapon : ResizerWeapon
    {
        public override void PrimaryAction()
        {
            var hit = DoRayCast();

            if (hit.TryGetComponent<Shrinkable>(out var resizable))
                ResizeInternal(resizable);
        }

        public override void SecondaryAction()
        {
            var shrinkable = GetComponentInParent<Shrinkable>();
            ResizePlayerInternal(shrinkable);
        }
    }
}
