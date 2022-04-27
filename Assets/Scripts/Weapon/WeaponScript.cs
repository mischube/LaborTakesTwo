using System;
using UnityEngine;

namespace Weapon
{
    public abstract class WeaponScript : MonoBehaviour
    {
        public WeaponContainer weaponContainer;
       
        protected GameObject Body;

        protected virtual void OnEnable()
        {
            //if weapon got picked up
            if (weaponContainer == null)
                return;

            Destroy(transform.GetChild(0).gameObject);
            Body = Instantiate(weaponContainer.body, transform.position, transform.rotation, transform);
            if (!Body.CompareTag("Player"))
                throw new Exception("Weapon body needs a tag");
        }

        public abstract void PrimaryAction();

        public abstract void SecondaryAction();
    }
}