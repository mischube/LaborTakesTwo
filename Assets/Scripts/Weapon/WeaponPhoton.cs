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
            var body = transform.GetChild(0);
            Destroy(body.gameObject);
            var weaponBody = Resources.Load<GameObject>(weaponName);
            Instantiate(weaponBody, transform.position, transform.rotation, transform);
        }
    }

    public delegate void WeaponSwitchHandler(string weaponName);
}