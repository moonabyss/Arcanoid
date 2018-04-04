using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    //public Texture2D cursor;
    public GameObject cursor;

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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowCursor(bool mode)
    {
        cursor.SetActive(mode);
    }
}
