using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>Interface for creating a game start rule.
    /// </summary>
    /// <param name="levelName"></param>
    /// <returns></returns>
    private IEnumerator StartGame(string levelName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName);
    }

    /// <summary>When Play button is pressed then scene of the main game is loaded.
    /// </summary>
    public void onLevelButtonPressed()
    {
        StartCoroutine(StartGame("SnakeScene"));
    }
}
