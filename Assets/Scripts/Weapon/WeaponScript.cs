using System;
using UnityEngine;

namespace Weapon
{
    public abstract class WeaponScript : MonoBehaviour
    {
        public WeaponContainer weaponContainer;

        private GameObject _body;


        protected virtual void OnEnable()
        {
            //if weapon got picked up
            if (weaponContainer == null)
                return;

            _body = transform.GetChild(0).gameObject;

            Destroy(_body.gameObject);

            var weaponPhoton = GetComponent<WeaponPhoton>();
            weaponPhoton.RaiseWeaponChangedEvent(weaponContainer.body.name);

            _body = Instantiate(weaponContainer.body, transform.position, transform.rotation, transform);

            if (!_body.CompareTag("Player"))
                throw new Exception("Weapon body needs a tag");
        }

        public abstract void PrimaryAction();

        public abstract void SecondaryAction();

        protected virtual void OnDisable()
        {
            return;
        }
    }
}