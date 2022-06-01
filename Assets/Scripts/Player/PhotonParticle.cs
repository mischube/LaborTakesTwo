using Photon.Pun;
using UnityEngine;

public class PhotonParticle : MonoBehaviourPun, IPunObservable
{
    public bool iceParticle;
    public bool fireParticle;
    public bool wateringParticle;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(iceParticle);
            stream.SendNext(fireParticle);
            stream.SendNext(wateringParticle);
        } else if (stream.IsReading)
        {
            iceParticle = (bool) stream.ReceiveNext();
            fireParticle = (bool) stream.ReceiveNext();
            wateringParticle = (bool) stream.ReceiveNext();
            SetParticle();
        }
    }

    private void SetParticle()
    {
        if (photonView.IsMine)
            return;
        if (transform.GetChild(0).gameObject.name.Equals("IceRodPrefab(Clone)")
            || transform.GetChild(0).gameObject.name.Equals("FireRodPrefab(Clone)"))
        {
            if (iceParticle || fireParticle)
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            } else
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
        }

        if (transform.GetChild(0).gameObject.name.Equals("Gießkanne(Clone)"))
        {
            if (wateringParticle)
            {
                transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().Play();
            } else
            {
                transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
