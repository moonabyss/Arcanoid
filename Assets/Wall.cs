using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        if (contact.collider.tag == "Ball")
        {
            Collider2D collider = contact.collider;
            Rigidbody2D rbCollider = collider.GetComponent<Rigidbody2D>();
            rbCollider.velocity = Vector2.Reflect(collider.GetComponent<Ball>().BallVelocity, contact.normal);
        }
    }
}
