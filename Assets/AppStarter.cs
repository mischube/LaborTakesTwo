using UnityEngine;

public static class AppStarter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    internal static void OnBeforeSceneLoadRuntimeMethod()
    {
        var gameManager = Resources.Load<GameObject>("GameManager");
        Object.Instantiate(gameManager);
    }
}