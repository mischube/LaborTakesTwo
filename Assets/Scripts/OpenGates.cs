using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGates : MonoBehaviour
{
    public GameObject door;
    public GameObject doorOnMountain;
    public GameObject mainGateA;
    public GameObject mainGateB;
    public float gateOpenWihdt = 4f;
    private bool closeBottomDoor;
    private bool closeTopDoor;
    private bool closeMainDoor= true;
    public float topDoorUpperLimit=14.6f;
    public float topDoorLowerLimit=8f;
    public float bottomDoorUpperLimit=3.6f;
    public float bottomDoorLowerLimit=-3.6f;
    
    // Update is called once per frame
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
        if (closeMainDoor == false) ;
        if (gameObject.name == "Button1" && closeMainDoor )
            closeBottomDoor = true;

        if (gameObject.name == "Button2" && closeMainDoor )
            closeTopDoor = true;
    }

    private void LowerGates()
    {
        if(closeBottomDoor)
            if (door.transform.position.y < bottomDoorUpperLimit)
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 0.05f,
                    door.transform.position.z);
            else
                closeBottomDoor = false;
            
        if(closeTopDoor)
            if (doorOnMountain.transform.position.y < topDoorUpperLimit)
                doorOnMountain.transform.position = new Vector3(doorOnMountain.transform.position.x,
                    doorOnMountain.transform.position.y+0.05f,doorOnMountain.transform.position.z) ;
            else
                closeTopDoor = false;
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
        if (closeTopDoor == false && closeMainDoor == false)
        {
            closeMainDoor = false;
            closeBottomDoor = false;
            closeTopDoor = false;
            if (mainGateA.transform.position.x <=mainGateA.transform.position.x + gateOpenWihdt) 
                mainGateA.transform.position = new Vector3(mainGateA.transform.position.x -0.1f, mainGateA.transform.position.y,
                    mainGateA.transform.position.z);
            if (mainGateB.transform.position.x >=mainGateB.transform.position.x - gateOpenWihdt)
                mainGateB.transform.position = new Vector3(mainGateB.transform.position.x +0.1f, mainGateB.transform.position.y,
                    mainGateB.transform.position.z);
        }
    }

    private void OpenUpperGate()
    {
        closeTopDoor = false;
        if (doorOnMountain.transform.position.y > topDoorLowerLimit)
            doorOnMountain.transform.position = new Vector3(doorOnMountain.transform.position.x,
                doorOnMountain.transform.position.y-0.05f,doorOnMountain.transform.position.z) ;
    }

    private void OpenLowerGate()
    {
        closeBottomDoor = false;
        if (door.transform.position.y > bottomDoorLowerLimit)
            door.transform.position = new Vector3(door.transform.position.x,door.transform.position.y-0.05f,
                door.transform.position.z) ;
    }

    private void OpenMainGate()
    {
        closeMainDoor = false;
        OpenUpperGate();
        OpenLowerGate();
    }
}
