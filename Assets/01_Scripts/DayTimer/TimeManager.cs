using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    
    public static int Minute{get; private set;}
    public static int Hour{get; private set;}

    private readonly float _minuteToRealTime = 1f;
    private float _timer;
    public static bool InTransition;
    
    void Start()
    {
        Minute = 00;
        Hour = 9;
        _timer=_minuteToRealTime;
        InTransition = true;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        OnMinuteChanged?.Invoke();
        
        if (_timer <= 0 && !NPCManager.IsDayOver && !InTransition)
        {
            Minute += 5;
            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }
            _timer=_minuteToRealTime;
        }
    }
}
