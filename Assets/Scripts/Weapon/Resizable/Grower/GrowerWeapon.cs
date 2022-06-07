namespace Weapon.Resizable.Grower
{
    public class GrowerWeapon : ResizerWeapon
    {
        public override void PrimaryAction()
        {
            throw new System.NotImplementedException();
        }

        public override void SecondaryAction()
        {
            var growable = GetComponentInParent<Growable>();
            ResizeInternal(growable);
        }
    }
}
