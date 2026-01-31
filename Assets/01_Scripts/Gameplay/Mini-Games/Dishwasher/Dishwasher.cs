using System;
using UnityEngine;

public class Dishwasher : MonoBehaviour
{
    [Header("Minigame")]
    [SerializeField] GameObject minigameHeader;
    
    private int _platesInMachine = 0;
    
    [SerializeField] GameObject[] plates = new GameObject[3];

    private void OnDisable()
    {
        _platesInMachine = 0;
        foreach (GameObject plate in plates)
        {
            plate.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate"))
        {
            _platesInMachine++;
            
            if (_platesInMachine >= 3)
            {
                MinigameManager.Instance.MiniGameEnd();
                MaintenanceManager.dishwasherState = MaintenanceManager.WashingMachineState.HasCleaning;
                minigameHeader.SetActive(false);
            }
        }
    }
}
