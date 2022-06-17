using UnityEngine;

public class OnDestroyDoThis : MonoBehaviour
{
    private Door.Door door;
    
    private void OnDestroy()
    {
        door.Open();
    }
}
