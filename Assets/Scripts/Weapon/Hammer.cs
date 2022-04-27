using System;
using UnityEngine;
using System.Collections;

namespace Weapon
{
    public class Hammer : WeaponScript
    {
        private Animator _animator;
        private bool _currentlyAttacking = false;
        private float _hitboxActiveTime =1f;
        public override void PrimaryAction()
        {
            _currentlyAttacking = true;
            _animator.SetTrigger("Hit");
            StartCoroutine(ActivateHitbox());
        }
        
        public override void SecondaryAction()
        {
            _animator.SetTrigger("Push");
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = weaponContainer.animatorController;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0)) 
                PrimaryAction();
            if (Input.GetMouseButtonDown(1))
                SecondaryAction();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (weaponContainer == null)
                return;
            Body.AddComponent<BoxCollider>();
            Body.GetComponent<BoxCollider>().isTrigger = true;
            //Body.GetComponent<BoxCollider>().enabled = false;
            Body.AddComponent<Rigidbody>();
            Body.GetComponent<Rigidbody>().useGravity = false;
            //Body.GetComponent<BoxCollider>().enabled = false;
            
            HammerHit Hammerhit = Body.AddComponent<HammerHit>();
            Hammerhit.hammer = this;

        }
        IEnumerator ActivateHitbox()
        {
            yield return new WaitForSeconds(_hitboxActiveTime);
            _currentlyAttacking = false;
        }

        public bool getAttackActive()
        {
            return _currentlyAttacking;
        }
    }
}