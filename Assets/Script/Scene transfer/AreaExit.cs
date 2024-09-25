using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController3 playerController = other.gameObject.GetComponent<PlayerController3>();
        if (playerController != null)
        {
            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);

            // Set the transition name in the scene management system
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        }
    }
}
