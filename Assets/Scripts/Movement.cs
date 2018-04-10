using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Range(0.1f,0.5f)]
    public float acceleration = .4f;
    private const float rightWall = 5.5f;
    private const float leftWall = -5.5f;
    public float minX = -5.5f;
    public float maxX = 5.5f;

    Vector2 playerPosition;

    // Use this for initialization
    void Start ()
    {
        maxX = rightWall - GetComponent<SpriteRenderer>().bounds.size.x / 2;
        minX = leftWall + GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    private void Update()
    {
        Vector3 playerPosition = transform.position;

        // mouse control
        playerPosition.x = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, playerPosition.y)).x;

        /*
        // keyboard control
        float x = Input.GetAxis("Horizontal");
        playerPosition.x += x * acceleration;
        */

        playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);
        transform.position = playerPosition;
    }

}
