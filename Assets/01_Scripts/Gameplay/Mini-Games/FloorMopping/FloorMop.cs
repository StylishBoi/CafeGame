using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FloorMop : MonoBehaviour
{
    //Sweep mechanics
    private bool _repeat;
    private readonly int _sweepsTotal = 10;
    private int _sweepsCount;

    //Sprite animation
    [Header("Sprite Animation")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    
    [Header("Minigame")]
    [SerializeField] private GameObject minigameHeader;
    
    //Coloring
    private float _currentOpacity = 1f;
    private Color _color;

    private void Start()
    {
        leftArrow.SetActive(false);
    }

    private void OnDisable()
    {
        _currentOpacity = 1f;
        spriteRenderer.color = new Color(_color.r, _color.g, _color.b, _currentOpacity);
        _sweepsCount = 0;
        _repeat = false;
    }
    void FixedUpdate()
    {
        if (MinigameInput.Instance.GetMoveLPressed() && _repeat)
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.sweepSFX);
            transform.localPosition += new Vector3(-80f, 0f, 0f)*Time.deltaTime;
            SweepEvent();
        }
        else if (MinigameInput.Instance.GetMoveRPressed() && !_repeat)
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.sweepSFX);
            transform.localPosition += new Vector3(80f, 0f, 0f)*Time.deltaTime;
            SweepEvent();
        }

        if (_sweepsCount >= _sweepsTotal)
        {
            MinigameManager.Instance.MiniGameEnd();
            MaintenanceManager.RemoveMaintenanceEvent();
            minigameHeader.SetActive(false);
        }
    }

    void SweepEvent()
    {
        //Prepare for next sweep
        _repeat=!_repeat;
        _sweepsCount++;
        leftArrow.SetActive(_repeat);
        rightArrow.SetActive(!_repeat);
        
        //Modify the opacity
        _color=spriteRenderer.color;
        _currentOpacity -= (1f/_sweepsTotal);
        spriteRenderer.color = new Color(_color.r, _color.g, _color.b, _currentOpacity);
    }
}
