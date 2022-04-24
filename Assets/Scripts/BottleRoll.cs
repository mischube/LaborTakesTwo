using UnityEngine;

public class BottleRoll : MonoBehaviour
{
    public new GameObject gameObject;
    public Vector3 rollingSpeed;

    void Start()
    {
        rollingSpeed.y = 0.5f;
        rollingSpeed.x = 0;
        rollingSpeed.z = 0;
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Rotate(rollingSpeed.x, -rollingSpeed.y, rollingSpeed.z);
            gameObject.transform.position = new Vector3
            (gameObject.transform.position.x + 0.051f,
                gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Rotate(rollingSpeed.x, rollingSpeed.y, rollingSpeed.z);
            gameObject.transform.position = new Vector3
            (gameObject.transform.position.x - 0.051f,
                gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}