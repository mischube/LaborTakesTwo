using UnityEngine;

namespace Door
{
    public abstract class Door : MonoBehaviour
    {
        [SerializeField] protected DoorDirection direction = DoorDirection.Horizontal;
        [SerializeField] protected float openingWidth;
        [SerializeField] protected float speed;
        [SerializeField] protected bool movePositiveDir;


        public abstract void Open();

        public abstract void Close();
    }
}
