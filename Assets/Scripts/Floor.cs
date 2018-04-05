using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball")
        {
            Destroy(collision.gameObject);
            gameManager.player.BallLost();
        }
    }
}
