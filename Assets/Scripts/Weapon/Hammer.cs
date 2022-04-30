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
        private float _pushingActiveTime =1.5f;
        private bool _currentlyPushing = false;
        private bool _swing = false;
        public override void PrimaryAction()
        {
            _animator.SetTrigger("Hit");
            StartCoroutine(ActivateHitbox());
        }

        public override void SecondaryAction()
        {
            if (!_swing)
                Swing();
            if (_swing)
                Load();
            StartCoroutine(ActivatePushing());
        }
        private void Load()
        {
            _animator.SetBool("Loading", true);
        } 
        private void Swing()
        {
            _animator.SetTrigger("Charge");     
            _swing = true;
        }   
        private void StopAnimation()
        {
            _animator.SetBool("Loading", false);
            _animator.ResetTrigger("Charge");
            _swing = false;
            _currentlyPushing = false;
        }
               
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = weaponContainer.animatorController;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && !_currentlyAttacking)
                PrimaryAction();
            if (Input.GetMouseButtonDown(1)&& !_currentlyPushing)
                SecondaryAction();
            if (Input.GetMouseButtonUp(1))
                StopAnimation();
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
            _currentlyAttacking = true;
            yield return new WaitForSeconds(_hitboxActiveTime);
            _currentlyAttacking = false;
        }
        IEnumerator ActivatePushing()
        {
            yield return new WaitForSeconds(_pushingActiveTime);
            _currentlyPushing = true;
        }
        public bool getAttackActive()
        {
            return _currentlyAttacking;
        }
        public bool getPushActive()
        {
            return _currentlyPushing;
        }
    }
}