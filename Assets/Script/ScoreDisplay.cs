using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : Singleton<ScoreDisplay>
{
    private TMP_Text scoreText;

    const string COIN_AMOUNT_TEXT = "Score Text";
    float displayScore = 0;
    public float transitionSpeed = 100;
    private void Update()
    {
        displayScore = Mathf.MoveTowards(displayScore, ScoreManager.Instance.Score, transitionSpeed * Time.deltaTime);
        UpdateScoreText();
    }
    public void UpdateScoreText()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        scoreText.text = "Score: " + ScoreManager.Instance.Score;
    }
}
