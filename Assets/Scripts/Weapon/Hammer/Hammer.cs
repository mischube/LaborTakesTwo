using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Weapon.Hammer.Script
{
    public class Hammer : WeaponScript
    {
        private Animator _animator;
        private bool _currentlyAttacking = false;
        private float _hitboxActiveTime = 2f;
        private float _pushingActiveTime = 1.5f;
        private bool _currentlyPushing = false;
        private bool _swing = false;

        public override void PrimaryAction()
        {
            if (_currentlyAttacking)
                return;

            _animator.SetTrigger("Hit");
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
            //_animator = transform.root.GetComponent<Animator>();
            _animator.runtimeAnimatorController = weaponContainer.animatorController;
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

            var photonAnimatorView = transform.root.GetComponent<PhotonAnimatorView>();
            photonAnimatorView.SetParameterSynchronized(
                "Hit",
                PhotonAnimatorView.ParameterType.Trigger,
                PhotonAnimatorView.SynchronizeType.Discrete);
            photonAnimatorView.SetParameterSynchronized(
                "Charge",
                PhotonAnimatorView.ParameterType.Trigger,
                PhotonAnimatorView.SynchronizeType.Discrete);
            photonAnimatorView.SetParameterSynchronized(
                "Loading",
                PhotonAnimatorView.ParameterType.Bool,
                PhotonAnimatorView.SynchronizeType.Discrete);

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

        public bool GetAttackActive()
        {
            return _currentlyAttacking;
        }

        public bool GetPushActive()
        {
            return _currentlyPushing;
        }
    }
}
