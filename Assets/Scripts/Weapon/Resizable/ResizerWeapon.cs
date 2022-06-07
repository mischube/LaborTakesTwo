namespace Weapon.Resizable
{
    public abstract class ResizerWeapon : WeaponScript
    {
        public abstract override void PrimaryAction();

        public abstract override void SecondaryAction();


        protected void ResizeInternal(Resizable resizable)
        {
            resizable.Resize();
        }
    }
}
