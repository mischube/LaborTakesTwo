using System.Collections;
using Library;
using Player;
using UnityEngine;

namespace Weapon.Resizable
{
    public abstract class ResizerWeapon : WeaponScript
    {
        [SerializeField] protected float interactionDistance = 20f;
        [SerializeField] protected float thickness = 0.25f;
        [SerializeField] protected LayerMask interactableMask = ~0;
        [SerializeField] protected float cooldown = 0.75f;

        private LineRenderer _lineRenderer;
        private PlayerFocus _playerFocus;
        private bool _isReady = true;

        public abstract override void PrimaryAction();

        protected GameObject DoRayCast()
        {
            if (!_isReady)
                return null;

            _isReady = false;

            var position = transform.position + Vector3.up;
            var direction = _playerFocus.GetViewDirection();
            var destinationPos = position + direction * interactionDistance;

            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, position);
            _lineRenderer.SetPosition(1, destinationPos);

            if (Physics.SphereCast(
                    position,
                    thickness,
                    direction,
                    out var hit,
                    interactionDistance,
                    interactableMask))
            {
                StartCoroutine(nameof(ResetLineRenderer));
                return hit.transform.gameObject;
            }

            StartCoroutine(nameof(ResetLineRenderer));
            return null;
        }


        public abstract override void SecondaryAction();


        protected override void OnEnable()
        {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = thickness;
            _lineRenderer.endWidth = thickness;
            _playerFocus = GetComponentInParent<PlayerFocus>();
            base.OnEnable();
        }

        private void OnDisable()
        {
            Destroy(_lineRenderer);
        }

        protected void ResizeInternal(Resizable resizable)
        {
            resizable.Resize();
        }


        protected void ResizePlayerInternal(Resizable resizable)
        {
            ResizeInternal(resizable);
            UpdatePlayerStats(resizable.ResizeFactor);
        }


        private void UpdatePlayerStats(float scaleDiff)
        {
            var player = PlayerNetworking.LocalPlayerInstance;

            var playerMovement = player.GetComponent<Player.PlayerMovement>();
            playerMovement.characterController.stepOffset *= scaleDiff;

            playerMovement.speed *= scaleDiff;
            playerMovement.dashSpeed *= scaleDiff;
            playerMovement.jumpHeight *= scaleDiff;
        }


        private IEnumerator ResetLineRenderer()
        {
            yield return new WaitForSeconds(cooldown);

            _lineRenderer.Reset();
            _isReady = true;
        }
    }
}
