using UnityEngine;
using UnityEngine.SceneManagement;

public class BossUI : MonoBehaviour
{
    public GameObject victoryScene;
    public BossHealth bossHealth;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        //ich wüsste sonst nicht wie ich an bossHealth kommen könnte
        bossHealth = FindObjectOfType<BossHealth>();

        if (bossHealth == null)
            return;

        bossHealth.victoryEvent += ShowVictoryScreen;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Boss"))
        {
            bossHealth = FindObjectOfType<BossHealth>();
            bossHealth.victoryEvent += ShowVictoryScreen;
        }
    }

    private void ShowVictoryScreen()
    {
        victoryScene.SetActive(true);
    }

    private void OnDisable()
    {
        if (bossHealth == null)
            return;
        
        bossHealth.victoryEvent -= ShowVictoryScreen;
    }
}
