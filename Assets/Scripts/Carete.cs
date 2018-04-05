﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carete : MonoBehaviour {
    public GameObject ball;
    public float thrust;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = ball.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (rb.velocity == Vector2.zero)
            {
                float angle = Vector2.Angle(Camera.main.ScreenToWorldPoint(Input.mousePosition) - ball.transform.position, new Vector2(1, 0));
                if (angle <= 90 && angle >= 80)
                {
                    angle -= Random.Range(5f, 10f);
                }
                else if (angle >= 90 && angle <= 100)
                {
                    angle += Random.Range(5f, 10f);
                }
                Vector2 vector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

                ball.transform.SetParent(null);
                // Cursor position to angle. Angle to vector2
                rb.AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * thrust);

                GameManager.instance.ShowCursor(false);
            } 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        if (contact.collider.tag == "Wall")
        {

        }
    }

}