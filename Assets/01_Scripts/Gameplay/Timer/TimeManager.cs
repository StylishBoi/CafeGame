using UnityEngine;
using System;


public class TimeManager : MonoBehaviour
{
    [Serializable]
    public struct InGameTime
    {
        public int hour;
        public int minute;
    }

    [Serializable]
    public struct RushHour
    {
        public InGameTime startHour;
        public InGameTime endHour;
    }

    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnRushStart;
    public static Action OnRushOver;

    private InGameTime _currentTime;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    [Header("Time Schedules")]
    [SerializeField] public InGameTime startDayHour;
    [SerializeField] public InGameTime endDayHour;
    [SerializeField] public RushHour[] rushHours = new RushHour[2];

    private readonly float _minuteToRealTime = 1f;
    private float _timer;

    void Start()
    {
        Hour = startDayHour.hour;
        Minute = startDayHour.minute;

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
            _currentTime.hour = Hour;
            _currentTime.minute = Minute;
        }

        CheckRushes();

        if (CompareHours(endDayHour, _currentTime) && GameManager.Instance.State == GameState.CafePlay)
        {
            GameManager.Instance.SwitchState(GameState.BasicPlay);
            CafeUIManager.Instance.DayOver();
        }
    }

    private void CheckRushes()
    {
        for (int i = 0; i < rushHours.Length; i++)
        {
            if (CompareHours(rushHours[i].startHour, _currentTime))
            {
                OnRushStart?.Invoke();
            }
            else if (CompareHours(rushHours[i].endHour, _currentTime))
            {
                OnRushOver?.Invoke();
            }
        }
    }

    private bool CompareHours(InGameTime a, InGameTime b)
    {
        return (a.hour == b.hour && a.minute == b.minute);
    }
}