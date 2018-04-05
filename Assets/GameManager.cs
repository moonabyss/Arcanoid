using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    //public Texture2D cursor;
    public GameObject cursor;
    public GameObject cell;
    public GameObject caretePrefab;
    public Text UiLives;

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

        for (int x = -8; x <= 8; x++)
        {
            Instantiate(cell, new Vector3((float)x, 3, 0), Quaternion.identity);
        }

        player = new Player(startLives);

        player.OnLivesChanged += PlayersLivesChanged;
        UiLives.text = "Lives: " + player.Lives;
        PlayerAtInitialPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
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
    }

    private void PlayerAtInitialPosition()
    {
        carete =  Instantiate(caretePrefab, new Vector3(0, -4, 0), Quaternion.identity);
        player.BallAdded(1);
    }
}
