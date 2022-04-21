using UnityEngine;

namespace Weapon
{
    public abstract class WeaponScript : MonoBehaviour
    {
        public WeaponContainer weaponContainer;

        public bool isActive;
        

        public abstract void PrimaryAction();

        public abstract void SecondaryAction();
    }
}