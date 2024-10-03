using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
	public GameObject gameOverMenu;
	public GameObject chapterMenu;
	public GameObject sceneMenu;
	private Text buttonText;            // Reference to the text of the button inside the Chapter Menu

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

	public void ChapterMenu()
	{
		gameOverMenu.SetActive(false);
		chapterMenu.SetActive(true);
	}

	public void SceneMenu()
	{
		GameObject optionChapter = chapterMenu.transform.Find("Option Chapter")?.gameObject;
		Button chapterButton = optionChapter.transform.Find("Chapter1")?.GetComponent<Button>();
		// If Text is null, try TextMeshPro
		TMPro.TMP_Text tmpText = chapterButton.GetComponentInChildren<TMPro.TMP_Text>();
		if (tmpText != null)
		{
			Debug.Log("TextMeshPro text found: " + tmpText.text);
		}
		else
		{
			Debug.LogError("No text component found on the button");
		}
		chapterMenu.SetActive(false);
		sceneMenu.SetActive(true);
	}

	public void LoadScene()
	{
		sceneMenu.SetActive(false);
		SceneManager.LoadScene("Scene 3.1");
	}
}
