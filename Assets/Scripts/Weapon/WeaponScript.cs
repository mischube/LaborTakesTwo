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
            var script = transform.root.GetComponent<WeaponPhoton>();

            script.DeleteGameObject(transform.GetChild(0).gameObject);
            script.DestroyOldWeaponPun();
            script.SaveGameObject(weaponContainer, transform);
            script.ChangeWeaponPun(weaponContainer.body.name);
            //Body = Instantiate(weaponContainer.body, transform.position, transform.rotation, transform);

            //Führt zu errors, schau mal drüber mike ka wofür das ist
            //if (!Body.CompareTag("Player"))
            // throw new Exception("Weapon body needs a tag");
        }

        public abstract void PrimaryAction();

        public abstract void SecondaryAction();
    }
}