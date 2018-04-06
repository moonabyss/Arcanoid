using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int CellCount { get; set; }

    //public Texture2D cursor;
    public GameObject cursor;
    public GameObject cell;
    public GameObject caretePrefab;
    public Text UiLives;
    public GameObject InfoPanel;

    public int startLives = 3;

    public Player player;

    private GameObject carete;

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
        //Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;

        for (int x = -5; x <= 5; x++)
        {
            Instantiate(cell, new Vector3((float)x, -1, 0), Quaternion.identity);
            CellCount++;
        }

        player = new Player(startLives);

        player.OnLivesChanged += PlayersLivesChanged;
        Cell.OnCellDestroyed += CellDestroyed;
        UiLives.text = "Lives: " + player.Lives;
        PlayerAtInitialPosition();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
	}

    public void ShowCursor(bool mode)
    {
        cursor.SetActive(mode);
    }

    private void PlayersLivesChanged()
    {
        UiLives.text = "Lives: " + player.Lives;

        if (player.Balls == 0)
        {
            Destroy(carete);
        }

        if (player.Balls == 0 && player.Lives > 0)
        {
            PlayerAtInitialPosition();
        }

        if (player.Balls == 0 && player.Lives == 0)
        {
            GameOver();
            print(player.Lives);
        }
    }

    private void PlayerAtInitialPosition()
    {
        carete =  Instantiate(caretePrefab, new Vector3(0, -10, 0), Quaternion.identity);
        player.BallAdded(1);
        ShowCursor(true);
    }

    private void CellDestroyed()
    {
        CellCount--;
        if (CellCount == 0)
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
        InfoPanel.GetComponentInChildren<Text>().text = "Game Over!";
        InfoPanel.SetActive(true);
    }
}
