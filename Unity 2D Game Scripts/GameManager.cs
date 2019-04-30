using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        GS_PAUSEMENU,
        GS_GAME,
        GS_LEVELCOMPLETED,
        GS_GAME_OVER
    }

    public GameState currentGameState = GameState.GS_PAUSEMENU;

    public static GameManager instance;

    public Canvas pauseMenuCanvas;

    public Canvas inGameCanvas;

    public Canvas levelCompletedCanvas;

    public Canvas gameOverCanvas;


    public Image[] keysTab;
    public Image[] heartsTab;


    public Text coinsText;
    public Text coinsLevelCompletedText;
    public Text enemiesText;
    public Text enemiesLevelCompletedText;
    public Text livesLevelCompletedText;
    public Text gameplayTimeText;
    public Text scoreLevelCompletedText;

    private int coins = 0;
    public int keys = 0;
    public int hearts = 2;
    private int enemies = 0;
    private float timer = 0;
    public double timerScore = 0;
    public int livesComplete = 3;

    public int maxKeyNumber = 3;
    public bool keysCompleted = false;



    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.GS_PAUSEMENU);
        levelCompletedCanvas.enabled = (currentGameState == GameState.GS_LEVELCOMPLETED);
        gameOverCanvas.enabled = (currentGameState == GameState.GS_GAME_OVER);

        inGameCanvas.gameObject.SetActive((currentGameState == GameState.GS_GAME));
        pauseMenuCanvas.gameObject.SetActive((currentGameState == GameState.GS_PAUSEMENU));
        levelCompletedCanvas.gameObject.SetActive((currentGameState == GameState.GS_LEVELCOMPLETED));
        gameOverCanvas.gameObject.SetActive((currentGameState == GameState.GS_GAME_OVER));
    }    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    void Awake()
    {
        instance = this;

        InGame();

        coinsText.text = coins.ToString();

        enemiesText.text = enemies.ToString();

        for (int i = 0; i < keysTab.Length; i++)
            keysTab[i].color = Color.grey;

        livesLevelCompletedText.text = livesComplete.ToString();
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape) && currentGameState==GameState.GS_PAUSEMENU)
        {
            InGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentGameState==GameState.GS_GAME)
        {
            PauseMenu();
        }

        timer = timer + Time.deltaTime;
        

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");
        gameplayTimeText.text = minutes + ":" + seconds;

        
    }

    

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackToMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitGameButtonClicked()
    {
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    public void OnNextLevelButtonClicked()
    {
        SceneManager.LoadScene("Poziom2");
    }

    public void addKeys(int keysNumber)
    {
        keysTab[keys].color = Color.white;
        keys += 1;
        if (keys == keysNumber)
        {
            keysCompleted = true;
        }
    }

    public void addCoins(int coinNumber)
    {
        coins += coinNumber;
        coinsText.text = coins.ToString();
        coinsLevelCompletedText.text = coins.ToString();      
    }

    public void addScore(double timerScoreNumber)
    {
        timerScore = (coins*10000)/timer;
        scoreLevelCompletedText.text = timerScore.ToString("0000");
    }

    public void addEnemies(int enemyNumber)
    {
        enemies += enemyNumber;
        enemiesText.text = enemies.ToString();
        enemiesLevelCompletedText.text = enemies.ToString();
    }

    public void addHearts(int heartsNumber)
    {
        heartsTab[hearts].enabled = true;
        hearts += 1;
        livesComplete += 1;
        livesLevelCompletedText.text = livesComplete.ToString();
    }

    public void substractHearts(int heartsNumber)
    {
        heartsTab[hearts].enabled = false;
        hearts -= 1;
        livesComplete -= 1;
        livesLevelCompletedText.text = livesComplete.ToString();
    }

}
