using Global;
using Library.StringEnums;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AppStartup
{
    public static Scenes Scene;


    /// <summary>
    /// Secures that start scene is loaded every game
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    internal static void OnBeforeSceneLoadRuntimeMethod()
    {
        const Scenes startScene = Scenes.Start;

        var currentScene = SceneManager.GetActiveScene();

        if (currentScene.GetEnumValue() == startScene)
            return;

        Scene = currentScene.GetEnumValue();
        Debug.Log(Scene);
        SceneManager.LoadScene(startScene.GetStringValue());
    }
}