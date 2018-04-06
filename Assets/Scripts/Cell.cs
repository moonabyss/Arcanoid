using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    private int durability;

    public delegate void CellDestroyed();
    public static event CellDestroyed OnCellDestroyed;

    // Use this for initialization
    void Start ()
    {
        durability = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball")
        {
            durability--;
            if (durability == 0)
            {
                Destroy(gameObject);
                OnCellDestroyed();
            }
        }
    }
}
