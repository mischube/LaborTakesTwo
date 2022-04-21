using Photon.Pun;
using UnityEngine;

namespace UI
{
    public class InteractableInfo : MonoBehaviourPun
    {
        public float interactionDistance;
        public Camera cam;
        public LayerMask interactableMask;
        public bool itemHoveredNow;

        private RaycastHit _hit;
        private GameObject _hitObject;
        private bool _itemHoveredOnce;
        private Ray _ray;
        private TextBoxScript _textBoxScript;

        void Update()
        {
            _ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            if (Physics.Raycast(_ray, out _hit, interactionDistance, interactableMask))
            {
                _textBoxScript = _hit.transform.gameObject.GetComponent<TextBoxScript>();
                _hitObject = _hit.transform.gameObject;
                SetInteractableTextRotation();
                ShowInteractableText();
                _itemHoveredOnce = true;
                itemHoveredNow = true;
            }

            if (_itemHoveredOnce && !Physics.Raycast(_ray, out _hit, interactionDistance, interactableMask))
            {
                _textBoxScript.SetDescription("");
                itemHoveredNow = false;
            }
        }

        void ShowInteractableText()
        {
            _textBoxScript.SetDescription(_textBoxScript.GetDescription());
        }

        void SetInteractableTextRotation()
        {
            _textBoxScript.SetTextRotation(cam.transform.rotation);
        }

        public GameObject GetHoveredItem()
        {
            return _hitObject;
        }
    }
}