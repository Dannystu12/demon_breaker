using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int numScenes = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = currentSceneIndex == (numScenes - 1) ? 0 : currentSceneIndex + 1;
        if(nextSceneIndex == 0)
        {
            FindObjectOfType<GameSession>().DestroySelf();
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}  
