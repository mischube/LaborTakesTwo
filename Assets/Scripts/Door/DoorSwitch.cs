using System;
using System.Collections.Generic;
using UnityEngine;

namespace Door
{
    public class DoorSwitch : MonoBehaviour
    {
        [SerializeField] private Door door;
        [SerializeField] private bool closeOnLeave;
        [SerializeField] private String[] possibleTags;
        private bool openedOnce;

        private void OnTriggerEnter(Collider other)
        {
            if (openedOnce && !closeOnLeave)
                return;
            
            foreach (var wantedTag in possibleTags)
            {
                if (other.transform.tag.Equals(wantedTag))
                {
                    openedOnce = true;
                    door.Open();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!closeOnLeave)
            {
                return;
            }

            foreach (var wantedTag in possibleTags)
            {
                if (other.transform.tag.Equals(wantedTag))
                {
                    door.Close();
                }
            }
        }
    }
}
