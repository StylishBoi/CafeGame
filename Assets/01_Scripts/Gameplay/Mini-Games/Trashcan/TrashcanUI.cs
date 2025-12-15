using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashcanUI : MonoBehaviour
{
    [SerializeField] private List<Image> trashcanIcons;
    void Update()
    {
        if (TrashcanMinigame.trashCount == 1)
        {
            trashcanIcons[0].color = Color.white;
        }
        else if (TrashcanMinigame.trashCount == 2)
        {
            trashcanIcons[1].color = Color.white;
        }
        else if (TrashcanMinigame.trashCount == 3)
        {
            trashcanIcons[2].color = Color.white;
        }
    }
}
