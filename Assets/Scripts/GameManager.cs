using UnityEngine;

public class GameManager : MonoBehaviour
{
    public VictoryZone victoryZone;

    private void Start()
    {
        victoryZone.PlayerEvent += OnPlayerEnteredZone;
    }

    private void OnPlayerEnteredZone(object sender)
    {
       Debug.Log("Victory!!!!!!!!!");
    }
}
