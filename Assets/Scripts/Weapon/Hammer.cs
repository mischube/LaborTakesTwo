using System;
using UnityEngine;
using System.Collections;

namespace Weapon
{
    public class Hammer : WeaponScript
    {
        private Animator _animator;
        private bool _currentlyAttacking = false;
        private float _hitboxActiveTime =2f;
        //private bool chargeCooldown = false;
        public override void PrimaryAction()
        {
            _currentlyAttacking = true;
            _animator.SetTrigger("Hit");
            StartCoroutine(ActivateHitbox());
        }

        public override void SecondaryAction()
        {
            _animator.SetTrigger("Charge");
        }
        /*
               public override void SecondaryAction()
               {
                   
                   _animator.SetBool("Loading", true);
                   StartCoroutine(ChargeCooldown());
                   if (chargeCooldown)
                   {
                       Debug.Log("inside");
                       _animator.SetBool("Canceling", true);
                   }
                       
               }
              
               private void StopAnimation()
               {
                   _animator.SetBool("Loading", false);
                   chargeCooldown = false;
               }
               */
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = weaponContainer.animatorController;
        }

        private void Update()
        {
            if(Input.GetMouseButton(0)) 
                PrimaryAction();
            if(Input.GetMouseButton(1)) 
                SecondaryAction();
            //if (Input.GetMouseButtonDown(1))
            //    SecondaryAction();
            //if (Input.GetMouseButtonUp(1))
            //    StopAnimation();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (weaponContainer == null)
                return;
            Body.AddComponent<BoxCollider>();
            Body.GetComponent<BoxCollider>().isTrigger = true;
            Body.AddComponent<Rigidbody>();
            Body.GetComponent<Rigidbody>().useGravity = false;

            HammerHit Hammerhit = Body.AddComponent<HammerHit>();
            Hammerhit.hammer = this;

        }
        IEnumerator ActivateHitbox()
        {
            yield return new WaitForSeconds(_hitboxActiveTime);
            _currentlyAttacking = false;
        }
        /*
        IEnumerator ChargeCooldown()
        {
            yield return new WaitForSeconds(_hitboxActiveTime);
            chargeCooldown = true;
        }
        */
        public bool getAttackActive()
        {
            return _currentlyAttacking;
        }
    }
}