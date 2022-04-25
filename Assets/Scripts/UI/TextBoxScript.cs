using UnityEngine;
using Weapon;

namespace UI
{
    public class TextBoxScript : MonoBehaviour
    {
        public TextMesh interactionText;

        private WeaponScript _weapon;

        private void Start()
        {
            _weapon = GetComponent<WeaponScript>();
        }

        public void SetTextRotation(Quaternion rotation)
        {
            interactionText.transform.rotation = rotation;
        }

        public void ShowDescription()
        {
            interactionText.text = _weapon.weaponContainer.description;
        }

        public void HideDescription()
        {
            interactionText.text = "";
        }
    }
}