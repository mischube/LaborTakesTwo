using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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
        private PhotonView photonView;

        public override void PrimaryAction()
        {
            if (_currentlyAttacking)
                return;
            Debug.Log("Primary");
            SendPlayAnimationEvent(photonView.ViewID, "Hit", "Trigger");
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
            SendPlayAnimationEvent(photonView.ViewID, "Loading", "true");
        }

        private void Swing()
        {
            SendPlayAnimationEvent(photonView.ViewID, "Charge", "Trigger");
            _swing = true;
        }

        private void StopAnimation()
        {
            SendPlayAnimationEvent(photonView.ViewID, "Loading", "false");
            SendPlayAnimationEvent(photonView.ViewID, "Charge", "TriggerNegative");
            _swing = false;
            _currentlyPushing = false;
        }

        private void Start()
        {
            _animator = transform.root.GetComponent<Animator>();
            photonView = transform.root.GetComponent<PhotonView>();
            _animator.runtimeAnimatorController = weaponContainer.animatorController;
            
        }

        public const byte PlayAnimationEventCode = 1;

        private void SendPlayAnimationEvent(
            int photonViewID,
            string animatorParameter,
            string parameterType,
            object parameterValue = null)
        {
            object[] content = {photonViewID, animatorParameter, parameterType, parameterValue};
            Debug.Log(content);
            Debug.Log("" + photonViewID + "" + animatorParameter + "" + parameterType + "" + parameterValue);
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
            PhotonNetwork.RaiseEvent(PlayAnimationEventCode, content, raiseEventOptions, SendOptions.SendReliable);
        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(1))
                StopAnimation();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
            if (weaponContainer == null)
                return;

            var hammerHit = gameObject.GetComponentInChildren<HammerHit>();
            hammerHit.hammer = this;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        private void OnEvent(EventData photonEvent)
        {
            Debug.Log("Event owo");
            Debug.Log(photonEvent);
            byte eventCode = photonEvent.Code;
            if (eventCode == PlayAnimationEventCode)
            {
                object[] data = (object[]) photonEvent.CustomData;
                int targetPhotonView = (int) data[0];
                if (targetPhotonView == this.photonView.ViewID)
                {
                    string animatorParameter = (string) data[1];
                    string parameterType = (string) data[2];
                    object parameterValue = (object) data[3];
                    switch (parameterType)
                    {
                        case "Trigger":
                            _animator.SetTrigger(animatorParameter);
                            break;
                        case "Bool":
                            _animator.SetBool(animatorParameter, (bool) parameterValue);
                            break;
                        case "TriggerNegative":
                            _animator.ResetTrigger(animatorParameter);
                            break;
                    }
                }
            }
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