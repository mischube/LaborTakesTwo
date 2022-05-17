using Library.Exceptions;
using UnityEngine;

namespace Global
{
    /// <summary>
    /// Add this script to game objects which should be cross-scene.
    /// </summary>
    public class CrossSceneObject : MonoBehaviour
    {
        private void Awake()
        {
            if (CompareTag("Untagged"))
                throw new InvalidTagException($"{tag} is not allowed on {gameObject}");

            var objs = GameObject.FindGameObjectsWithTag(tag);

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}