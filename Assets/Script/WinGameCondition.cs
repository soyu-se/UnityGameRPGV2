using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WinGameCondition : MonoBehaviour
{
    private EnemyHealth bossHealth;
    public GameObject WinGameScene;
    public TMP_Text scoreText;
    public TMP_Text timeCounter;
    private void Start()
    {
        WinGameScene.SetActive(false);
        bossHealth = GameObject.Find("Boss Dark").GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if (bossHealth != null)
        {
            if (bossHealth.currentHealth <= 0)
            {
                WinGame();
            }
        }
    }
    private void WinGame()
    {        
        float score = ScoreManager.Instance.Score;        
        TimeSpan finishTime = Timer.Instance.playingTime;
        
        scoreText.text = "Score: " + score; 
        timeCounter.text = "Finish Time: " + finishTime.ToString("mm':'ss'.'ff");
        Time.timeScale = 0;
        WinGameScene.SetActive(true);
    }
}
