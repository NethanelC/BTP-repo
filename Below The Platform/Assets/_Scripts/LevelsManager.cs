using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public void LoadAScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
