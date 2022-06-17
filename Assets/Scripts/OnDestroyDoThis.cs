using UnityEngine;

public class OnDestroyDoThis : MonoBehaviour
{
    [SerializeField]
    private Door.Door door;
    
    private void OnDestroy()
    {
        door.Open();
    }
}
