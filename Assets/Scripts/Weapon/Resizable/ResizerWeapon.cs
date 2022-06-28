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
        [SerializeField] protected LayerMask interactableMask;
        [SerializeField] protected float cooldown = 0.5f;

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
            _lineRenderer.positionCount = 0;
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


        private IEnumerator ResetLineRenderer()
        {
            yield return new WaitForSeconds(cooldown);

            _lineRenderer.Reset();
            _isReady = true;
        }

        public override void CopyParameters(WeaponScript original)
        {
            base.CopyParameters(original);

            if (!(original is ResizerWeapon resizerWeapon))
                return;

            interactableMask = resizerWeapon.interactableMask;
            interactionDistance = resizerWeapon.interactionDistance;
            cooldown = resizerWeapon.cooldown;
        }
    }
}
