﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {

    private const float VELOCITY = 2f;

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

            // ball goes up
            if (oldVelocity.y > 0 && oldVelocity.y < 0.5)
            {
                oldVelocity.y += VELOCITY;
            }
            // ball goes down
            if (oldVelocity.y < 0 && oldVelocity.y > -0.5)
            {
                oldVelocity.y -= VELOCITY;
            }
            // ball goes right
            if (oldVelocity.x > 0 && oldVelocity.x < 0.5)
            {
                oldVelocity.x += VELOCITY;
            }
            // ball goes left
            if (oldVelocity.x < 0 && oldVelocity.x > -0.5)
            {
                oldVelocity.x -= VELOCITY;
            }
            contact.rigidbody.velocity = Vector2.Reflect(oldVelocity, contact.normal);
            GetComponent<Collider2D>().isTrigger = true;
            Invoke("EnableCollider", 0.05f);
        }
    }

    private void EnableCollider()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }

}
