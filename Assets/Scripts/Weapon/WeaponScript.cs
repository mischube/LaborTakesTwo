using UnityEngine;

namespace Weapon
{
    public abstract class WeaponScript : MonoBehaviour
    {
        public WeaponContainer weaponContainer;


        public abstract void PrimaryAction();

        public abstract void SecondaryAction();
    }
}