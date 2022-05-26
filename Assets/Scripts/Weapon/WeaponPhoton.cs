using System;
using UnityEngine;

namespace Weapon
{
    public class WeaponPhoton : MonoBehaviour
    {
        public event WeaponSwitchHandler WeaponSwitched;

        public void RaiseWeaponChangedEvent(string weaponName)
        {
            WeaponSwitched?.Invoke(weaponName);
        }


        public void ChangeWeapon(string weaponName)
        {
            var weaponContainer = Resources.Load<WeaponContainer>($"Weapon\\{weaponName}");

            if (weaponContainer == null)
                throw new Exception($"could not load weapon container: {weaponName}");

            ChangeBody(weaponContainer);
            ChangeAnimatorController(weaponContainer);
        }

        private void ChangeAnimatorController(WeaponContainer weaponContainer)
        {
            var root = transform.root;
            var animator = root.GetComponent<Animator>();

            var animatorController = weaponContainer.animatorController;
            animator.runtimeAnimatorController = animatorController;
        }

        private void ChangeBody(WeaponContainer weaponContainer)
        {
            var body = transform.GetChild(0);

            if (body != null)
                Destroy(body.gameObject);

            var weaponBody = weaponContainer.body;
            Instantiate(weaponBody, transform.position, transform.rotation, transform);
        }
    }

    public delegate void WeaponSwitchHandler(string weaponName);
}
