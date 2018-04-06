using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        if (contact.collider.tag == "Ball")
        {
            Vector2 oldVelocity = contact.collider.GetComponent<Ball>().BallVelocity;
            contact.rigidbody.velocity = Vector2.Reflect(oldVelocity, contact.normal);
        }
    }
}
