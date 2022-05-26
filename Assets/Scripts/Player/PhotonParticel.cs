using Photon.Pun;

public class PhotonParticel : MonoBehaviourPun, IPunObservable
{
    public bool iceParticel;
    public Icerod icerod;
    
    public bool fireParticel;
    public FireRod firerod;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(iceParticel);
            stream.SendNext(fireParticel);
        }
        else if (stream.IsReading)
        {
            iceParticel = (bool) stream.ReceiveNext();
            fireParticel = (bool) stream.ReceiveNext();
            setIceParticel();
        }
    }

    public void setIceParticel()
    {
        if (!photonView.IsMine && (transform.GetChild(0).gameObject.name.Equals("IceRodPrefab(Clone)") 
                                   || transform.GetChild(0).gameObject.name.Equals("FireRodPrefab(Clone)")))
        {
            if (iceParticel || fireParticel)
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            } 
            else
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
