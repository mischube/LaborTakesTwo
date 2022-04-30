using System;
using Global;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Scenes destinationScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.SwitchScene(destinationScene);
        }
    }
}