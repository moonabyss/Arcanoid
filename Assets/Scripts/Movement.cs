using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Range(0.1f,0.5f)]
    public float acceleration = .4f;
    public float minX = -7.25f;
    public float maxX = 7.25f;

    Vector2 playerPosition;

    // Use this for initialization
    void Start () {
        
	}

    private void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //playerPosition.x += x * acceleration;

        Vector3 playerPosition = transform.position;
        playerPosition.x = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, playerPosition.y)).x;
        playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);
        transform.position = playerPosition;
    }

}
