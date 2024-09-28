using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public void EnableGameMenu()
    {
        gameOverMenu.SetActive(true);
    }
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += EnableGameMenu;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= EnableGameMenu;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Scene 3.1");
        gameOverMenu.SetActive(false);
    }
}
