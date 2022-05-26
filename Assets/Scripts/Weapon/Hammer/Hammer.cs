using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Weapon.Hammer
{
    public class Hammer : WeaponScript
    {
        [SerializeField] private float hitboxActiveTime = 2f;
        [SerializeField] private float pushingActiveTime = 1.5f;

        private bool _currentlyAttacking;
        private bool _currentlyPushing;
        private bool _swing;
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Loading = Animator.StringToHash("Loading");
        private static readonly int Charge = Animator.StringToHash("Charge");


        public override void PrimaryAction()
        {
            if (_currentlyAttacking)
                return;

            Animator.SetTrigger(Hit);
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
            Animator.SetBool(Loading, true);
        }

        private void Swing()
        {
            Animator.SetTrigger(Charge);
            _swing = true;
        }

        private void StopAnimation()
        {
            Animator.SetBool(Loading, false);
            Animator.ResetTrigger(Charge);
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
            yield return new WaitForSeconds(hitboxActiveTime);
            _currentlyAttacking = false;
        }

        IEnumerator ActivatePushing()
        {
            yield return new WaitForSeconds(pushingActiveTime);
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
