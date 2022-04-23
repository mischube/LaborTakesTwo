using System;
using UnityEngine;

namespace Weapon
{
    public abstract class WeaponScript : MonoBehaviour
    {
        public WeaponContainer weaponContainer;


        private void OnEnable()
        {
            //if weapon got picked up
            if (weaponContainer == null)
                return;

            Destroy(transform.GetChild(0).gameObject);
            var body = Instantiate(weaponContainer.body, transform.position, transform.rotation, transform);

            if (!body.CompareTag("Player"))
                throw new Exception("Weapon body needs a tag");
        }

        public abstract void PrimaryAction();

        public abstract void SecondaryAction();
    }
}