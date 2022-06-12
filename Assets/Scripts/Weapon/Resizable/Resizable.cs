using UnityEngine;

namespace Weapon.Resizable
{
    public abstract class Resizable : MonoBehaviour
    {
        [SerializeField] protected float resizeFactor;

        public float ResizeFactor => resizeFactor;

        public abstract void Resize();

        protected void ResizeInternal()
        {
            transform.localScale *= resizeFactor;
        }
    }
}
