using UnityEngine;

public class BossUI : MonoBehaviour
{
    public GameObject victoryScene;
    public BossHealth bossHealth;

    private void Start()
    {
        //ich wüsste sonst nicht wie ich an bossHealth kommen könnte
        bossHealth = FindObjectOfType<BossHealth>();
        bossHealth.victoryEvent += ShowVictoryScreen;
    }

    private void ShowVictoryScreen()
    {
        victoryScene.SetActive(true);
    }

    private void OnDisable()
    {
        bossHealth.victoryEvent -= ShowVictoryScreen;
    }
}
