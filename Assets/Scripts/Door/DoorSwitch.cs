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
        [SerializeField] private bool shouldOnlyOpenOnce;
        private bool openedOnce;

        private void OnTriggerEnter(Collider other)
        {
            if (openedOnce && !closeOnLeave)
                return;

            if (shouldOnlyOpenOnce)
                openedOnce = true;

            if (possibleTags.Length == 0)
            {
                door.Open();
            }
            
            foreach (var wantedTag in possibleTags)
            {
                if (other.transform.tag.Equals(wantedTag))
                {
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
            
            if (possibleTags.Length == 0)
            {
                door.Close();
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
