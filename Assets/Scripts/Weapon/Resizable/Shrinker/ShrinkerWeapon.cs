namespace Weapon.Resizable.Shrinker
{
    public class ShrinkerWeapon : ResizerWeapon
    {
        public override void PrimaryAction()
        {
            throw new System.NotImplementedException();
        }

        public override void SecondaryAction()
        {
            var shrinkable = GetComponentInParent<Shrinkable>();
            ResizeInternal(shrinkable);
        }
    }
}
