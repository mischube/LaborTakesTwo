using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "Weapon")]
    public class WeaponContainer : ScriptableObject
    {
        public new string name;

        public string description;

        public Sprite sprite;

        public Sprite icon;
    }
}