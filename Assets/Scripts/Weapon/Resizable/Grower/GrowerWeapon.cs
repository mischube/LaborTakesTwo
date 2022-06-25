namespace Weapon.Resizable.Grower
{
    public class GrowerWeapon : ResizerWeapon
    {
        public override void PrimaryAction()
        {
            var hit = DoRayCast();

            if (hit == null)
                return;

            if (hit.transform.TryGetComponent<Growable>(out var resizable))
                ResizeInternal(resizable);
        }

        public override void SecondaryAction()
        {
            var growable = GetComponentInParent<Growable>();
            ResizePlayerInternal(growable);
        }
    }
}
