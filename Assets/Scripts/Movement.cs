using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private const float rightWall = 5.5f;
    private const float leftWall = -5.5f;
    public float minX = -5.5f;
    public float maxX = 5.5f;

    //Vector2 playerPosition;

    private Vector2 lastMousePosition;

    // Use this for initialization
    void Start ()
    {
        GetBounce();
    }

    private void Update()
    {
        if (!GameManager.instance.InPause && GameManager.GetGameMode() == GameMode.PLAYING)
        {

            Vector3 playerPosition = transform.position;

            // mouse control
            playerPosition.x += (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x - lastMousePosition.x;

            playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);
            transform.position = playerPosition;
        }
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void GetBounce()
    {
        maxX = rightWall - GetComponent<SpriteRenderer>().bounds.size.x / 2;
        minX = leftWall + GetComponent<SpriteRenderer>().bounds.size.x / 2;

        Vector3 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);
        transform.position = playerPosition;
    }

}
