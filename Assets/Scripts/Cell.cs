using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        if (contact.collider.tag == "Ball")
        {
            Destroy(gameObject);
        }
    }
}
