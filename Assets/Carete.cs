using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carete : MonoBehaviour {
    public GameObject ball;
    public float thrust;

    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = ball.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb.velocity == Vector3.zero)
            {
                rb.AddForce(new Vector3(1, 1, 0) * thrust);
            } 
        }
    }
}
