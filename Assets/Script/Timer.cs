using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{
    public TMP_Text timeCounter;

    private TimeSpan playingTime;
    private bool timerGoing;

    private float elapseTime;

    protected override void Awake()
    {
        base.Awake();
        timeCounter = GameObject.Find("Timer text").GetComponent<TMP_Text>();
    }
    private void Start()
    {
        timeCounter.text = "Time: 00:00:00";
        timerGoing = false;
    }
    public void BeginTimer()
    {
        timerGoing = true;
        elapseTime = 0;
        StartCoroutine(UpdateTimer());
    }
    public void EndTimer()
    {
        timerGoing = false;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapseTime += Time.deltaTime;
            playingTime = TimeSpan.FromSeconds(elapseTime);
            string playingTimeStr = "Time: " + playingTime.ToString("mm':'ss'.'ff");
            timeCounter.text = playingTimeStr;

            yield return null;
        }
    }
}

