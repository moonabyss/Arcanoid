using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb;
    public Vector2 BallVelocity { get; private set;  }

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void FixedUpdate()
    {
        if (rb.velocity != Vector2.zero)
        {
            BallVelocity = rb.velocity;
        }
        
    }

}
