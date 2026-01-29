using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashcanUI : MonoBehaviour
{
    [SerializeField] private List<Image> trashcanIcons;
    void Update()
    {
        if (TrashcanMinigame.TrashCount == 1)
        {
            trashcanIcons[0].color = Color.white;
        }
        else if (TrashcanMinigame.TrashCount == 2)
        {
            trashcanIcons[1].color = Color.white;
        }
        else if (TrashcanMinigame.TrashCount == 3)
        {
            trashcanIcons[2].color = Color.white;
        }
    }
}
