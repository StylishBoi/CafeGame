using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct RushHour
{
    public int startHour;
    public int endHour;
}

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    [SerializeField] public int startDayHour;
    [SerializeField] public int endDayHour;
    [SerializeField] public RushHour[] rushHours=new RushHour[2];

    private readonly float _minuteToRealTime = 1f;
    private float _timer;

    void Start()
    {
        Hour = startDayHour;
        _timer = _minuteToRealTime;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        OnMinuteChanged?.Invoke();

        if (_timer <= 0 && GameManager.Instance.State == GameState.CafePlay)
        {
            Minute += 5;
            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            _timer = _minuteToRealTime;
        }
    }
}