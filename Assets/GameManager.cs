using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public bool InPause { get; private set; }

    //public Texture2D cursor;
    public GameObject cursor;
    public GameObject cell_blue;
    public GameObject cell_yellow;
    public GameObject cell_green;
    public GameObject cell_magenta;
    public GameObject cell_red;
    public GameObject cell_gray;
    public GameObject caretePrefab;
    public ScoreAdd addScorePrefab;
    public Text UiLives;
    public Text UiScores;
    public GameObject InfoPanel;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public Transform levelHandler;

    public int startLives = 3;

    public Player player;
    public Vector2 lastMousePosition;

    private GameObject carete;

    private int cellCount = 0;
    private int score = 0;

    private void Awake()
    {
        if (gameObject != null && gameObject != this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        InPause = false;
        //Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;

        CreateLevel(1);

        player = new Player(startLives);

        player.OnLivesChanged += PlayersLivesChanged;
        Cell.OnCellDestroyed += CellDestroyed;
        UiLives.text = "Life\n" + player.Lives;
        PlayerAtInitialPosition();

        Button[] buttons = PauseMenu.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            switch (button.name)
            {
                case "ContinueButton" : button.onClick.AddListener(delegate { SwitchPause(); }); break;
                case "ExitButton": button.onClick.AddListener(delegate { Quit(); }); break;
                default: break;
            }
        }

        buttons = GameOverMenu.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            switch (button.name)
            {
                case "PlayAgainButton": button.onClick.AddListener(delegate { PlayAgain(); }); break;
                case "ExitButton": button.onClick.AddListener(delegate { Quit(); }); break;
                default: break;
            }
        }

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("escape"))
        {
            SwitchPause();
            //Application.Quit();
        }
	}

    public void ShowCursor(bool mode)
    {
        //cursor.SetActive(mode);
        cursor.SetActive(false);
    }

    private void PlayersLivesChanged()
    {
        UiLives.text = "Life\n" + player.Lives;

        if (player.Balls == 0)
        {
            Destroy(carete);
        }

        if (player.Balls == 0 && player.Lives > 0)
        {
            Invoke("PlayerAtInitialPosition", 1.5f);
        }

        if (player.Balls == 0 && player.Lives == 0)
        {
            GameOver();
        }
    }

    private void PlayerAtInitialPosition()
    {
        carete =  Instantiate(caretePrefab, new Vector3(0, -11, 0), Quaternion.identity);
        player.BallAdded(1);
        //ShowCursor(true);
    }

    private void CellDestroyed(int score, Transform position)
    {
        cellCount--;
        ScoreChanged(score);
        ScoreAdd go =  Instantiate(addScorePrefab, position.position + new Vector3(0, 0, -1), Quaternion.identity);
        go.scoreText.text = GetSign(score) + score.ToString();
        if (cellCount == 0)
        {
            LevelCompleted();
        }
    }
    private void LevelCompleted()
    {
        GameObject[] collection = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in collection)
        {
            Destroy(ball);
        }
        Destroy(carete);
        InfoPanel.GetComponentInChildren<Text>().text = "Level Completed!";
        InfoPanel.SetActive(true);
    }

    private void GameOver()
    {
        //InfoPanel.GetComponentInChildren<Text>().text = "Game Over!";
        //InfoPanel.SetActive(true);
        GameOverMenu.SetActive(true);
        Cursor.visible = true;
    }

    private void ScoreChanged(int score)
    {
        this.score += score;
        UiScores.text = "Score\n" + this.score.ToString();
    }

    private void CreateLevel(int level)
    {
        for (int x = -5; x <= 5; x++)
        {
            Instantiate(cell_gray, new Vector3((float)x, -1f, 0), Quaternion.identity, levelHandler);
            cellCount++;
            Instantiate(cell_red, new Vector3((float)x, -1.5f, 0), Quaternion.identity, levelHandler);
            cellCount++;
            Instantiate(cell_magenta, new Vector3((float)x, -2f, 0), Quaternion.identity, levelHandler);
            cellCount++;
            Instantiate(cell_yellow, new Vector3((float)x, -2.5f, 0), Quaternion.identity, levelHandler);
            cellCount++;
            Instantiate(cell_blue, new Vector3((float)x, -3f, 0), Quaternion.identity, levelHandler);
            cellCount++;
            Instantiate(cell_green, new Vector3((float)x, -3.5f, 0), Quaternion.identity, levelHandler);
            cellCount++;
        }
    }

    private string GetSign(int value)
    {
        if (value > 0)
            return "+";
        else if (value < 0)
            return "-";
        else
            return "";

    }

    private void ShowPauseMenu(bool display)
    {
        PauseMenu.SetActive(display);
        Cursor.visible = display;
    }

    private void SwitchPause()
    {
        InPause = !InPause;
        Time.timeScale = InPause ? 0 : 1;

        if (InPause)
        {
            ShowPauseMenu(true);
        }
        else
        {
            ShowPauseMenu(false);
        }
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void PlayAgain()
    {
        ClearLevel();
        cellCount = 0;
        CreateLevel(1);
        GameOverMenu.SetActive(false);
        Cursor.visible = false;
        PlayerAtInitialPosition();
        player.AddLife(startLives);
        score = 0;
        ScoreChanged(score);
    }

    private void ClearLevel()
    {
        for (int i = 0; i < levelHandler.childCount; i++)
        {
            Destroy(levelHandler.GetChild(i).gameObject);
        }
    }
}
