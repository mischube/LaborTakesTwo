using Global;
using UnityEngine;

namespace Weapon.Hammer
{
    public class Destroyable : MonoBehaviour
    {
        public void Destroy()
        {
            GameManager.Instance.DestroyObject(gameObject);
        }
    }
}
