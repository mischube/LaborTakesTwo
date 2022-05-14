using System.Collections;
using UnityEngine;

namespace Weapon.Hammer.Script
{
    public class Hammer : WeaponScript
    {
        private bool _currentlyAttacking;
        private float _hitboxActiveTime = 2f;
        private float _pushingActiveTime = 1.5f;
        private bool _currentlyPushing;
        private bool _swing;

        
        public override void PrimaryAction()
        {
            if (_currentlyAttacking)
                return;

            Animator.SetTrigger("Hit");
            StartCoroutine(ActivateHitbox());
        }

        public override void SecondaryAction()
        {
            if (_currentlyPushing)
                return;

            if (!_swing)
                Swing();
            if (_swing)
                Load();

            StartCoroutine(ActivatePushing());
        }

        private void Load()
        {
            Animator.SetBool("Loading", true);
        }

        private void Swing()
        {
            Animator.SetTrigger("Charge");
            _swing = true;
        }

        private void StopAnimation()
        {
            Animator.SetBool("Loading", false);
            Animator.ResetTrigger("Charge");
            _swing = false;
            _currentlyPushing = false;
        }


        private void Update()
        {
            if (Input.GetMouseButtonUp(1))
                StopAnimation();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (weaponContainer == null)
                return;

            var hammerHit = gameObject.GetComponentInChildren<HammerHit>();
            hammerHit.hammer = this;
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
