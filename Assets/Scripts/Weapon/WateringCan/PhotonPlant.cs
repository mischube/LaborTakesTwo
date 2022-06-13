using Photon.Pun;
using UnityEngine;

public class PhotonPlant : MonoBehaviour, IPunObservable
{
    private GameObject plantPrefab;
    private Vector3 plantPosition;
    private Vector3 plantScale;
    private GameObject oldPlantParent;

    private bool currentlyAnPlant = false;
    private bool alreadyAnPlant = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentlyAnPlant);
            stream.SendNext(plantPosition);
            stream.SendNext(plantScale);
        } else if (stream.IsReading)
        {
            currentlyAnPlant = (bool) stream.ReceiveNext();
            plantPosition = (Vector3) stream.ReceiveNext();
            plantScale = (Vector3) stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (currentlyAnPlant)
            EnablePolymorph();
        else
        {
            DisablePolymorph(plantPosition);
        }
    }

    private void Start()
    {
        plantPrefab = (GameObject) Resources.Load("LeafPrefab");
    }

    public void SetPolyEnable(bool state)
    {
        currentlyAnPlant = state;
    }

    public void SetPolyPlant(Vector3 pos)
    {
        plantPosition = pos;
    }

    public void SetPlantSize(Vector3 scaling)
    {
        plantScale = scaling;
    }

    public void EnablePolymorph()
    {
        if (alreadyAnPlant)
            return;
        plantPrefab = Instantiate(plantPrefab);
        plantPrefab.transform.localScale = plantScale;
        transform.Find("Cylinder").gameObject.SetActive(false);
        transform.Find("Cube").gameObject.SetActive(false);
        transform.Find("Inventory").gameObject.SetActive(false);
        plantPrefab.transform.SetParent(transform);
        alreadyAnPlant = true;
    }

    public void DisablePolymorph(Vector3 oldPlantPos)
    {
        if (!alreadyAnPlant)
            return;
        plantPosition = oldPlantPos;
        transform.Find("Cylinder").gameObject.SetActive(true);
        transform.Find("Cube").gameObject.SetActive(true);
        transform.Find("Inventory").gameObject.SetActive(true);
        plantPrefab.transform.position = plantPosition;
        alreadyAnPlant = false;
    }
}
