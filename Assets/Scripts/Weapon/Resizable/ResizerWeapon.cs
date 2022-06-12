using Player;
using UnityEngine;

namespace Weapon.Resizable
{
    public abstract class ResizerWeapon : WeaponScript
    {
        [SerializeField] protected float interactionDistance = 20f;
        [SerializeField] protected LayerMask interactableMask = ~0;

        public abstract override void PrimaryAction();

        protected GameObject DoRayCast()
        {
            var cam = PlayerNetworking.LocalPlayerInstance.GetComponentInChildren<Camera>();
            var ray = new Ray(transform.position, cam.transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red, 5f);
            if (!Physics.Raycast(ray, out var hit, interactionDistance, interactableMask))
                return null;
            return hit.transform.gameObject;
        }

        public abstract override void SecondaryAction();


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

            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.characterController.stepOffset *= scaleDiff;

            playerMovement.speed *= scaleDiff;
            playerMovement.dashSpeed *= scaleDiff;
            playerMovement.jumpHeight *= scaleDiff;
        }
    }
}
