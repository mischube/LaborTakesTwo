using UnityEngine;

public class OpenGates : MonoBehaviour
{
    public GameObject door;
    public GameObject doorOnMountain;
    public GameObject mainGateA;
    public GameObject mainGateB;
    public float gateOpenWidth = 4f;

    private bool _closeBottomDoor;
    private bool _closeTopDoor;
    private bool _closeMainDoor = true;

    private const float TopDoorUpperLimit = 14.6f;
    private const float TopDoorLowerLimit = 8f;
    private const float BottomDoorUpperLimit = 3.6f;
    private const float BottomDoorLowerLimit = -3.6f;
    
    void Update()
    {
        LowerGates();
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        OpenGate();
    }

    private void OnTriggerExit(Collider other)
    {
        GateControl();
    }

    private void GateControl()
    {
        if (_closeMainDoor == false);
        if (gameObject.name == "Button1" && _closeMainDoor)
            _closeBottomDoor = true;

        if (gameObject.name == "Button2" && _closeMainDoor)
            _closeTopDoor = true;
    }

    private void LowerGates()
    {
        if (_closeBottomDoor)
            if (door.transform.position.y < BottomDoorUpperLimit)
                door.transform.position = new Vector3
                (door.transform.position.x, door.transform.position.y + 0.05f,
                    door.transform.position.z);
            else
                _closeBottomDoor = false;

        if (_closeTopDoor)
            if (doorOnMountain.transform.position.y < TopDoorUpperLimit)
                doorOnMountain.transform.position = new Vector3
                (doorOnMountain.transform.position.x,
                    doorOnMountain.transform.position.y + 0.05f, doorOnMountain.transform.position.z);
            else
                _closeTopDoor = false;
    }

    private void OpenGate()
    {
        if (gameObject.name == "Button1")
        {
            OpenLowerGate();
        }

        if (gameObject.name == "Button2")
        {
            OpenUpperGate();
        }

        if (gameObject.name == "Button3")
        {
            OpenMainGate();
        }

        if (_closeTopDoor == false &&
            _closeMainDoor == false)
        {
            _closeMainDoor = false;
            _closeBottomDoor = false;
            _closeTopDoor = false;
            if (mainGateA.transform.position.x <= mainGateA.transform.position.x + gateOpenWidth)
                mainGateA.transform.position = new Vector3
                (mainGateA.transform.position.x - 0.1f,
                    mainGateA.transform.position.y,
                    mainGateA.transform.position.z);
            if (mainGateB.transform.position.x >= mainGateB.transform.position.x - gateOpenWidth)
                mainGateB.transform.position = new Vector3
                (mainGateB.transform.position.x + 0.1f,
                    mainGateB.transform.position.y,
                    mainGateB.transform.position.z);
        }
    }

    private void OpenUpperGate()
    {
        _closeTopDoor = false;
        if (doorOnMountain.transform.position.y > TopDoorLowerLimit)
            doorOnMountain.transform.position = new Vector3
            (doorOnMountain.transform.position.x,
                doorOnMountain.transform.position.y - 0.05f, doorOnMountain.transform.position.z);
    }

    private void OpenLowerGate()
    {
        _closeBottomDoor = false;
        if (door.transform.position.y > BottomDoorLowerLimit)
            door.transform.position = new Vector3
            (door.transform.position.x, door.transform.position.y - 0.05f,
                door.transform.position.z);
    }

    private void OpenMainGate()
    {
        _closeMainDoor = false;
        OpenUpperGate();
        OpenLowerGate();
    }
}