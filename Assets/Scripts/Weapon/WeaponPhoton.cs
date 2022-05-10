using Photon.Pun;
using UnityEngine;
using Weapon;

public class WeaponPhoton : MonoBehaviourPun
{
    private GameObject selectedWeapon;
    private GameObject oldHandPosition;
    private WeaponContainer weapons;
    private Transform parentObject;

    public void ChangeWeaponPun(string name)
    {
        photonView.RPC("ChangeWeapon", RpcTarget.All, name);
    }

    public void DestroyOldWeaponPun()
    {
        photonView.RPC("DestroyWeapon", RpcTarget.All);
    }

    [PunRPC]
    public void ChangeWeapon(string name)
    {
        //Instantiate(weapons.body, parentObject.position, parentObject.rotation, parentObject);
        //oldHandPosition = transform.GetChild(5).GetChild(0).GetChild(0).gameObject;
        selectedWeapon = Instantiate(weapons.body, parentObject.position, parentObject.rotation, parentObject);
        selectedWeapon.transform.parent = parentObject;
    }

    [PunRPC]
    public void DestroyWeapon()
    {
        Destroy(selectedWeapon);
    }

    public GameObject ReturnGameObject()
    {
        return selectedWeapon;
    }

    public void DeleteGameObject(GameObject gameObject)
    {
        selectedWeapon = gameObject;
    }

    public void SaveGameObject(WeaponContainer weapon, Transform parent)
    {
        weapons = weapon;
        parentObject = parent;
    }
}