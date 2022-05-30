using Photon.Pun;

public class PhotonParticle : MonoBehaviourPun, IPunObservable
{
    public bool iceParticle;
    public Icerod icerod;
    
    public bool fireParticle;
    public FireRod firerod;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(iceParticle);
            stream.SendNext(fireParticle);
        }
        else if (stream.IsReading)
        {
            iceParticle = (bool) stream.ReceiveNext();
            fireParticle = (bool) stream.ReceiveNext();
            SetIceParticle();
        }
    }

    private void SetIceParticle()
    {
        if (!photonView.IsMine && (transform.GetChild(0).gameObject.name.Equals("IceRodPrefab(Clone)") 
                                   || transform.GetChild(0).gameObject.name.Equals("FireRodPrefab(Clone)")))
        {
            if (iceParticle || fireParticle)
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
