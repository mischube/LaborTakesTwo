using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxScript : MonoBehaviour
{
    public TextMesh interactionText;

    public string GetDescription()
    {
        return "Press <color=green>[F]</color> to Pickup";
    }

    public void  SetDescription(string description)
    {
        interactionText.text = description;
    }

    public void SetTextRotation(Quaternion rotation)
    {
        interactionText.transform.rotation = rotation;
    }
}
