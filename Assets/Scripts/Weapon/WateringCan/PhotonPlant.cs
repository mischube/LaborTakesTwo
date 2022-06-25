using Photon.Pun;
using UnityEngine;

public class PhotonPlant : MonoBehaviour, IPunObservable
{
    private GameObject plantPrefab;
    private GameObject plantModel;
    private GameObject oldPlantParent;

    private Vector3 plantPosition;
    private Vector3 plantScale;

    public bool currentlyAnPlant;
    private bool alreadyAnPlant;
    private bool ownerOfPlant;
    private bool whichPlantUsed;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentlyAnPlant);
            stream.SendNext(whichPlantUsed);
            stream.SendNext(plantPosition);
            stream.SendNext(plantScale);
        } else if (stream.IsReading)
        {
            currentlyAnPlant = (bool) stream.ReceiveNext();
            whichPlantUsed = (bool) stream.ReceiveNext();
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

    public void SetWhichPlant(bool state)
    {
        whichPlantUsed = state;
    }

    public void SetPolyPlant(Vector3 pos)
    {
        plantPosition = pos;
    }

    public void SetPolyOwner()
    {
        ownerOfPlant = true;
    }

    public void SetPlantSize(Vector3 scaling)
    {
        plantScale = scaling;
    }

    public void EnablePolymorph()
    {
        if (alreadyAnPlant)
            return;
        if (ownerOfPlant)
            return;
        if (!whichPlantUsed)
        {
            plantModel = Instantiate(plantPrefab, transform.position, plantPrefab.transform.rotation);
            plantModel.transform.localScale = plantScale;
            plantModel.transform.SetParent(transform);
        }
        transform.Find("Cylinder").gameObject.SetActive(false);
        transform.Find("Cube").gameObject.SetActive(false);
        transform.Find("Inventory").gameObject.SetActive(false);
        alreadyAnPlant = true;
    }

    public void DisablePolymorph(Vector3 oldPlantPos)
    {
        if (!alreadyAnPlant)
            return;
        if (ownerOfPlant)
            return;

        if (!whichPlantUsed)
        {
            plantPosition = oldPlantPos;
            plantPrefab.transform.position = plantPosition;
            Destroy(plantModel);
        }
        transform.Find("Cylinder").gameObject.SetActive(true);
        transform.Find("Cube").gameObject.SetActive(true);
        transform.Find("Inventory").gameObject.SetActive(true);
        alreadyAnPlant = false;
    }
}
