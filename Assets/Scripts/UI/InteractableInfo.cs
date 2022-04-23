using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

namespace UI
{
    public class InteractableInfo : MonoBehaviourPun
    {
        public float interactionDistance;
        public Camera cam;
        public LayerMask interactableMask;

        private RaycastHit _hit;
        private GameObject _hitObject;
        private Ray _ray;


        void Update()
        {
            _ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            if (Physics.Raycast(_ray, out _hit, interactionDistance, interactableMask))
            {
                _hitObject = _hit.transform.gameObject;
                var textBoxScript = _hitObject.GetComponent<TextBoxScript>();
                if (textBoxScript != null)
                    ShowInteractableText(textBoxScript);
            } else if (_hitObject != null)
            {
                var textBoxScript = _hitObject.GetComponent<TextBoxScript>();

                if (textBoxScript != null)
                    textBoxScript.HideDescription();

                _hitObject = null;
            }
        }

        void ShowInteractableText(TextBoxScript textBoxScript)
        {
            textBoxScript.ShowDescription();
            textBoxScript.SetTextRotation(cam.transform.rotation);
        }

        [CanBeNull]
        public GameObject GetHoveredItem()
        {
            return _hitObject;
        }
    }
}