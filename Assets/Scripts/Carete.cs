using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carete : MonoBehaviour {
    public GameObject ball;
    public float thrust = 400;

    private Rigidbody2D rb;

	// Use this for initialization
	void OnEnable()
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
                /*
                float angle = Vector2.Angle(Camera.main.ScreenToWorldPoint(Input.mousePosition) - ball.transform.position, new Vector2(1, 0));

                if ((angle <= 90 && angle >= 80) || (angle <= 180 && angle >= 160))
                {
                    angle -= Random.Range(20f, 30f);
                }
                else if ((angle >= 90 && angle <= 100) || (angle >= 0 && angle <= 20))
                {
                    angle += Random.Range(20f, 30f);
                }
                Vector2 vector = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                */
                float angle = Random.Range(60f, 120f);
                ball.transform.SetParent(null);
                // Cursor position to angle. Angle to vector2
                rb.AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * thrust);


                GameManager.instance.ShowCursor(false);
            } 
        }
    }

}
