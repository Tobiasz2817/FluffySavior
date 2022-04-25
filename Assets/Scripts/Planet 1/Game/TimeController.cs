using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class MyEvent : UnityEvent { }
public class TimeController : MonoBehaviour
{
    public MyEvent OnEvent;
    
    private Text countTimeText;
    private bool isGrowingUp;

    private int minutes = 0;
    private int decimalSeconds = 0;
    private int seconds = 0;

    [SerializeField] private bool startInvokeTime;
    
    [SerializeField] float startCountingTime;
    [SerializeField] float timeToEndCounting;
    
    [SerializeField] float lastStartCountingTime;
    [SerializeField] float lastToEndCounting;

   
    private void Awake()
    {
        TryGetComponent<Text>(out countTimeText);
        lastStartCountingTime = startCountingTime;
        lastToEndCounting = timeToEndCounting;
    }

    private void Start()
    {
        if (startInvokeTime)
        {
            StartCountDownTimer();
        }
    }

    private bool IsGrowing()
    {
        if(startCountingTime > timeToEndCounting) return false;
        else if(startCountingTime < timeToEndCounting) return true;

        return true;
    }
    public void StartCountDownTimer()
    {
        isGrowingUp = IsGrowing();

        ConvertedInputToVariableTime();

        if(isGrowingUp) InvokeRepeating("ChargeTime", 0f, 1f);
        else InvokeRepeating("CountDownTimer", 0f,1f);
    }

    public void StopCountTime()
    {
        if (IsInvoking())
        {
            CancelInvoke();
        }
    }
    private void ConvertedInputToVariableTime()
    {
        this.minutes = (int)startCountingTime;

        if(minutes == startCountingTime)
            return;

        string startCountingTimeStr = startCountingTime.ToString();
        string [] subs = startCountingTimeStr.Split(',');

        this.minutes = int.Parse(subs[0]);

        if(minutes >= 60)
        {
            minutes = 60;
            return;
        }

        string numbersBehindDot = subs[1];
        this.decimalSeconds = int.Parse(numbersBehindDot[0].ToString());

        if (decimalSeconds >= 6)
        {
            decimalSeconds = 6;
            return;
        }

        if (numbersBehindDot.Length <= 1) return;

        this.seconds = int.Parse(numbersBehindDot[1].ToString());   
    }
    private void CountDownTimer()
    {
        string split = minutes.ToString() + "," + decimalSeconds.ToString() + seconds.ToString();
        float time = float.Parse(split);
        if(time >= timeToEndCounting)
        {
            if(countTimeText != null)
                countTimeText.text = GetTextByTime();

            if (decimalSeconds == 0 && seconds == 0)
            {
                minutes--;
                decimalSeconds = 5;
                seconds = 10;
            }
            else if(seconds == 0)
            {
                decimalSeconds--;
                seconds = 10;
            }


            if(seconds > 0)
                seconds--;
        }
        else
        {
            ResetTime();

            OnEvent?.Invoke();
        }
    }
    private void ChargeTime()
    {
        string split = minutes.ToString() + "," + decimalSeconds.ToString() + seconds.ToString();
        float time = float.Parse(split);

        if(time <= timeToEndCounting)
        {
            if(countTimeText != null)
                countTimeText.text = GetTextByTime();
            

            if (decimalSeconds == 5 && seconds == 9)
            {
                minutes++;
                decimalSeconds = 0;
                seconds = -1;
            }
            else if (seconds == 9)
            {
                decimalSeconds++;
                seconds = -1;
            }

            seconds++;
        }
        else
        {

            ResetTime();

            OnEvent?.Invoke();
        }
    }
    private string GetTextByTime()
    {
        if (minutes > 0)
        {
            return minutes + ":" + decimalSeconds + seconds;
        }
        else if(decimalSeconds > 0)
        {
            return decimalSeconds + seconds.ToString();
        }
        else
        {
            return seconds.ToString();
        }
    }
    public void ResetTime()
    {
        if(IsInvoking())
        {
            CancelInvoke();
        }

        minutes = 0;
        decimalSeconds = 0;
        seconds = 0;

        startCountingTime = lastStartCountingTime;
        timeToEndCounting = lastToEndCounting;
    }
    public void InvokeEventInTime(float timeFrom, float timeTo)
    {
        ResetTime();

        startCountingTime = timeFrom;
        timeToEndCounting = timeTo;

        StartCountDownTimer();

    }

    public void AddTimeToEnd(float time)
    {
        timeToEndCounting += time;
        
        /*Debug.Log("New Ended time: " + timeToEndCounting);*/
    }

    public int GetActuallyTime()
    {
        int time = (minutes * 60) + (decimalSeconds * 10) + seconds;
        
        return time;
    }

    public int GetEndedTime()
    {
        string endTime = timeToEndCounting.ToString();
        string [] subs = endTime.Split(',');

        int fulltime = 0;
        
        for (int i = 0; i < subs.Length; i++)
        {
            if (!string.IsNullOrEmpty(subs[i]))
            {
                if (i == 0)
                {
                    fulltime += int.Parse(subs[i]) * 60;
                    continue;
                }

                fulltime += int.Parse(subs[i]);
            }
        }
        return fulltime;
    }
}
