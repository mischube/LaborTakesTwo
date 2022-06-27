using UnityEngine;

public class OnDestroyDoThis : MonoBehaviour
{
    [SerializeField] private Door.Door door;
    [SerializeField] private ThreeBlockDmg threeBlockDmg;

    private void OnDestroy()
    {
        if (door!=null)
        {
            door.Open();
        }
        
        if (threeBlockDmg != null)
        {
            threeBlockDmg.setCounterUp();
        }
    }
}
