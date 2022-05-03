using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class HealthbarUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public RectTransform heartSpawnPoint;

    private PlayerHealth playerHealth;
    [SerializeField]
    private List<GameObject> heartList = new List<GameObject>(); 

    private void Start()
    {
        playerHealth = PlayerNetworking.LocalPlayerInstance.GetComponent<PlayerHealth>();
        playerHealth.playerDmgEvent += LoseAHeart;
        playerHealth.playerDeadEvent += FillAllHearts;
        CreateHearts();
    }

    private void CreateHearts()
    {
        int width = Screen.width;
        float spaceSide = (float) width / 16;

        int height = Screen.height;
        float spaceTop = (float) height / 25;
        
        for (int i = 0; i < playerHealth.GetMaxHealth(); i++)
        {
            GameObject heartInstantiate = 
                Instantiate(heartPrefab, heartSpawnPoint.position + new Vector3(spaceSide * 1.5f + i * spaceSide,-spaceTop,0),
                    heartSpawnPoint.rotation, transform);
            heartList.Add(heartInstantiate);
        }
    }

    public void LoseAHeart(int currentHealth)
    {
        heartList[currentHealth].gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void FillAllHearts()
    {
        foreach (var heart in heartList)
        {
            heart.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        playerHealth.playerDmgEvent -= LoseAHeart;
        playerHealth.playerDeadEvent -= FillAllHearts;
    }
}
