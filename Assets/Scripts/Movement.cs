using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Range(0.1f,0.5f)]
    public float acceleration;
    public float minX = -7.25f;
    public float maxX = 7.25f;

    Vector2 playerPosition;

    // Use this for initialization
    void Start () {
        
	}

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 playerPosition = transform.position;
        playerPosition.x += x * acceleration;
        playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);
        transform.position = playerPosition;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        
	}
}
