using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
	public GameObject gameOverMenu;
	public GameObject chapterMenu;
	public void EnableGameMenu()
	{
		gameOverMenu.SetActive(true);
		Time.timeScale = 0;
		Timer.Instance.EndTimer();
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
		SceneManager.LoadScene(SceneToLoad);
		gameOverMenu.SetActive(false);
		Time.timeScale = 1;
		Timer.Instance.ResetTimer();
	}

	public void ChapterMenu()
	{
		gameOverMenu.SetActive(false);
		chapterMenu.SetActive(true);
	}

	//public void LoadChapter1()
	//{
	//	SceneManager.LoadScene("Scenes/Chapter1/1.1");
	//	chapterMenu.SetActive(false);
	//}

	//public void LoadChapter2()
	//{
	//	SceneManager.LoadScene("Scenes/Chapter1/2.1");
	//	chapterMenu.SetActive(false);
	//}

	//public void LoadChapter3()
	//{
	//	SceneManager.LoadScene("Scenes/Chapter3/Scene 3.1");
	//	chapterMenu.SetActive(false);
	//}

	public void LoadChapter(string buttonName)
	{
		switch (buttonName)
		{
			case "Chapter 1 Button":
				SceneManager.LoadScene("Scenes/Chapter1/1.1");
				break;
			case "Chapter 2 Button":
				SceneManager.LoadScene("Scenes/Chapter1/2.1");
				break;
			case "Chapter 3 Button":
				SceneManager.LoadScene("Scenes/Chapter3/Scene 3.1");
				break;
			default:
				Debug.LogError("Invalid chapter button name: " + buttonName);
				break;
		}
		chapterMenu.SetActive(false);
		sceneMenu.SetActive(true);
	}
}
