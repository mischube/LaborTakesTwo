using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private GameObject plantPrefab;
    private GameObject localplayer;
    private GameObject currentplant;
    private Vector3 currentStartPos;
    private List<GameObject> plantList = new List<GameObject>();
    private float plantRangeOffset = 1f;
    private float rightSide;
    private float leftside;
    private float directionSpeed = 0.01f;
    public PlantType plantType;
    
    void Update()
    {
        if (plantList.Count > 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (currentStartPos.y + plantRangeOffset > currentplant.transform.localPosition.y)
                    currentplant.transform.position += new Vector3(0, directionSpeed, 0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (leftside > 1)
                {
                    rightSide = -leftside;
                } else
                {
                    leftside += directionSpeed;
                    rightSide -= directionSpeed;
                }

                if (leftside < 1f)
                    currentplant.transform.position -= currentplant.transform.right / 100;
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (currentStartPos.y - plantRangeOffset < currentplant.transform.localPosition.y)
                    currentplant.transform.position -= new Vector3(0, directionSpeed, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (rightSide > 1)
                {
                    leftside = -rightSide;
                } else
                {
                    rightSide += directionSpeed;
                    leftside -= directionSpeed;
                }

                if (rightSide < 1f)
                    currentplant.transform.position += currentplant.transform.right / 100;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject save = Instantiate(
                plantPrefab,
                currentplant.transform.position + currentplant.transform.forward * plantRangeOffset,
                plantPrefab.transform.rotation,
                localplayer.transform);
            plantType.SetGrowingPlant(currentplant.transform.position + currentplant.transform.forward * plantRangeOffset);
            plantList.Add(save);
            currentplant = save;
            currentStartPos = save.transform.localPosition;
            rightSide = 0f;
            leftside = 0f;
        }
    }

    public void SetCurrentPlant(GameObject plant, GameObject soil, GameObject player, PlantType _plantType)
    {
        currentplant = plantPrefab = plant;
        localplayer = player;
        rightSide = 0f;
        leftside = 0f;
        plantType = _plantType;

    }

    public List<GameObject> ReturnPlantList()
    {
        return plantList;
    }

    public void clearList()
    {
        plantList.Clear();
    }
}
