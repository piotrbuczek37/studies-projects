using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>Initialization of each game status.
    /// </summary>
    public enum GameState
    {
        GS_GAME,
        GS_GAME_OVER
    }

    public GameState currentGameState = GameState.GS_GAME; //Initialization of the current state of the game
    public static GameManager instance; //Creating an instance of GameManager
    public Canvas inGameCanvas; //Definition of public canvas which can be changed in the editor
    public Canvas gameOverCanvas; //Definition of public canvas which can be changed in the editor

    public Text scoreText; //Definition of public text of points number which can be changed in the editor
    public Text gameOverScoreText; //Definition of public text of points number on GameOver canvas which can be changed in the editor
    private int score = 0; //Initialization of the current value of points

    /// <summary>The SetGameState() method sets a new game status, so that the right canvas can be seen.
    /// </summary>
    /// <param name="newGameState"></param>
    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        if(newGameState == GameState.GS_GAME)
        {
            inGameCanvas.enabled = true;
        }
        else if(newGameState == GameState.GS_GAME_OVER)
        {
            gameOverCanvas.enabled = true;
            inGameCanvas.enabled = false;
        }
    }

    /// <summary>The InGame() method sets a main game status.
    /// </summary>
    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }

    /// <summary>The GameOver() method sets a game over status and converts the number of points to GameOver canvas.
    /// </summary>
    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
        gameOverScoreText.text = score.ToString();
    }

    /// <summary>Awake() is a method which is performed when the game starts. An instance of the current object is set, also sets a main game status and that score points are visible on InGame canvas.
    /// </summary>
    void Awake()
    {
        instance = this;
        InGame();
        scoreText.text = score.ToString();
    }

    /// <summary>The addScore() method adds points to score and converts them to game canvas. 
    /// </summary>
    /// <param name="scoreNumber"></param>
    public void addScore(int scoreNumber)
    {
        score += scoreNumber;
        scoreText.text = score.ToString();
    }

    /// <summary>When Restart button is pressed then the scene of the active game is loaded again.
    /// </summary>
    public void onRestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>When Quit button is pressed then the scene of the main menu is loaded.
    /// </summary>
    public void onQuitButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
