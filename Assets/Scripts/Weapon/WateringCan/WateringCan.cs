using UnityEngine;
using Weapon;

public class WateringCan : WeaponScript
{
   [SerializeField] private ParticleSystem particleSystem;
   private void Update()
   {
      if (Input.GetMouseButtonUp(0))
         particleSystem.Stop();
   }
   
   public override void PrimaryAction()
   {
      particleSystem = transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>();
      particleSystem.Play();
   }

   public override void SecondaryAction()
   {
      Debug.Log("lul");
   }
}
