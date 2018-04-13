using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode { STARTING, PLAYING, GAMEOVER, LEVEL_FINISHED, BALL_LOST}

public class GameManager : MonoBehaviour {

    private const int MAX_ROUND = 2;

    public static GameManager instance;
    private static GameMode gameMode;
    private static int round = 1;

    public bool InPause { get; private set; }

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
    public Text UiRound;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public Text GameOverScore;
    public Text GameOverHiScore;
    public Transform levelHandler;
    public GameObject[] bonuses;

    public int startLives = 3;

    public Player player;

    public AudioClip ballLost;
    public AudioClip gameOver;
    public AudioClip newLevel;

    private GameObject carete;
    public GameObject soundPrefab;

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
        //PlayerPrefs.DeleteAll();

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
                case "PlayAgainButton": button.onClick.AddListener(delegate { PlayNewGame(); }); break;
                case "ExitButton": button.onClick.AddListener(delegate { Quit(); }); break;
                default: break;
            }
        }

        player = new Player();

        player.OnLivesChanged += PlayersLivesChanged;
        Cell.OnCellDestroyed += CellDestroyed;

        PlayNewGame();

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("escape") && gameMode == GameMode.PLAYING)
        {
            SwitchPause();
        }
	}

    public static GameMode GetGameMode()
    {
        return gameMode;
    }

    private void PlayersLivesChanged()
    {
        UiLives.text = "Life\n" + player.Lives;

        if (player.Balls == 0)
        {
            gameMode = GameMode.BALL_LOST;
            GameObject sound = Instantiate(soundPrefab, transform);
            sound.GetComponent<AudioSource>().clip = ballLost;
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
            Destroy(carete, sound.GetComponent<AudioSource>().clip.length);
        }

        if (player.Balls == 0 && player.Lives > 0)
        {
            Invoke("PlayerAtInitialPosition", 2f);
            Invoke("ShowReady", 2f);
            Invoke("HideRound", 5f);
            player.BallAdded(1);
        }

        if (player.Balls == 0 && player.Lives == 0)
        {
            gameMode = GameMode.GAMEOVER;
            GameObject sound = Instantiate(soundPrefab, transform);
            sound.GetComponent<AudioSource>().clip = gameOver;
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
            GameOver();
        }
    }

    private void PlayerAtInitialPosition()
    {
        carete =  Instantiate(caretePrefab, new Vector3(0, -11, 0), Quaternion.identity);
    }

    private void CellDestroyed(int score, Transform position)
    {
        cellCount--;
        ScoreChanged(score);
        ScoreAdd go =  Instantiate(addScorePrefab, position.position + new Vector3(0, 0, -1), Quaternion.identity, levelHandler);
        go.scoreText.text = GetSign(score) + score.ToString();

        Random.InitState(System.DateTime.Now.Millisecond);
        float chance = Random.Range(0, 100);
        if (chance < 20)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Instantiate(bonuses[(int)(Random.value * 100) % bonuses.Length], position.position + new Vector3(0, 0, -1), Quaternion.identity, levelHandler);
        }

        if (cellCount == 0)
        {
            LevelCompleted();
        }
    }
    private void LevelCompleted()
    {
        round++;

        GameObject[] collection = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject go in collection)
        {
            Destroy(go);
        }
        collection = GameObject.FindGameObjectsWithTag("Bonus");
        foreach (GameObject go in collection)
        {
            Destroy(go);
        }
        Destroy(carete);

        if (round > MAX_ROUND)
        {

        }
        else
        {
            CreateLevel();
            PlayerAtInitialPosition();
        }

    }

    private void GameOver()
    {
        int hiScore = 0;
        if (!PlayerPrefs.HasKey("hiscore"))
        {
            hiScore = score;
            PlayerPrefs.SetInt("hiscore", hiScore);
        }
        else
        {
            if (PlayerPrefs.GetInt("hiscore") > score)
            {
                hiScore = PlayerPrefs.GetInt("hiscore");
            }
            else
            {
                PlayerPrefs.SetInt("hiscore", hiScore);
                hiScore = score;
            }
        }

        GameOverScore.text = "Score: " + score;
        GameOverHiScore.text = "HiScore: " + hiScore;
        GameOverMenu.SetActive(true);
        Cursor.visible = true;
    }

    private void ScoreChanged(int score)
    {
        this.score += score;
        UiScores.text = "Score\n" + this.score.ToString();
    }

    private void CreateLevel()
    {
        switch (round)
        {
            case 1: Level_01(); break;
            case 2: Level_02(); break;
            default:            break;
        }
        ShowRound();
        GameObject sound = Instantiate(soundPrefab, transform);
        sound.GetComponent<AudioSource>().clip = newLevel;
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
        Invoke("HideRound", 3f);
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
        if (gameMode == GameMode.PLAYING)
        {
            PauseMenu.SetActive(display);
            Cursor.visible = display;
        }
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

    private void SetGameModePlaying()
    {
        gameMode = GameMode.PLAYING;
    }

    private void ShowRound()
    {
        UiRound.text = "ROUND  " + round;
        UiRound.gameObject.SetActive(true);
    }

    private void ShowReady()
    {
        UiRound.text = "READY";
        UiRound.gameObject.SetActive(true);
    }

    private void HideRound()
    {
        UiRound.gameObject.SetActive(false);
        SetGameModePlaying();
    }

    private void PlayNewGame()
    {
        ClearLevel();
        cellCount = 0;
        round = 1;
        CreateLevel();
        GameOverMenu.SetActive(false);
        Cursor.visible = false;
        PlayerAtInitialPosition();
        player.BallAdded(1);
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

    private void Level_01()
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

    private void Level_02()
    {
        for (int y = -1; y > -6; y--)
        {
            Instantiate(cell_yellow, new Vector3(-5, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_yellow, new Vector3(-5, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        Instantiate(cell_blue, new Vector3(-4f, -1.5f, 0), Quaternion.identity, levelHandler);
        cellCount++;
        for (int y = -2; y > -6; y--)
        {
            Instantiate(cell_blue, new Vector3(-4f, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_blue, new Vector3(-4f, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        for (int y = -2; y > -6; y--)
        {
            Instantiate(cell_green, new Vector3(-3, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_green, new Vector3(-3, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        Instantiate(cell_magenta, new Vector3(-2f, -2.5f, 0), Quaternion.identity, levelHandler);
        cellCount++;
        for (int y = -3; y > -6; y--)
        {
            Instantiate(cell_magenta, new Vector3(-2f, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_magenta, new Vector3(-2f, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        for (int y = -3; y > -6; y--)
        {
            Instantiate(cell_red, new Vector3(-1, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_red, new Vector3(-1, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        Instantiate(cell_yellow, new Vector3(0f, -3.5f, 0), Quaternion.identity, levelHandler);
        cellCount++;
        for (int y = -4; y > -6; y--)
        {
            Instantiate(cell_yellow, new Vector3(0f, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_yellow, new Vector3(0f, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        for (int y = -4; y > -6; y--)
        {
            Instantiate(cell_blue, new Vector3(1, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_blue, new Vector3(1, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        Instantiate(cell_green, new Vector3(2, -4.5f, 0), Quaternion.identity, levelHandler);
        cellCount++;
        for (int y = -5; y > -6; y--)
        {
            Instantiate(cell_green, new Vector3(2, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_green, new Vector3(2, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        for (int y = -5; y > -6; y--)
        {
            Instantiate(cell_magenta, new Vector3(3, y, 0), Quaternion.identity, levelHandler);
            Instantiate(cell_magenta, new Vector3(3, y - .5f, 0), Quaternion.identity, levelHandler);
            cellCount += 2;
        }

        Instantiate(cell_red, new Vector3(4, -5.5f, 0), Quaternion.identity, levelHandler);
        cellCount++;

        for (int x = -5; x <= 5; x++)
        {
            Instantiate(cell_gray, new Vector3((float)x, -6f, 0), Quaternion.identity, levelHandler);
            cellCount++;
        }
    }

    private void Level_00()
    {
        for (int x = -5; x <= 5; x++)
        {
            Instantiate(cell_green, new Vector3((float)x, -3.5f, 0), Quaternion.identity, levelHandler);
        }
        cellCount++;
    }
}
