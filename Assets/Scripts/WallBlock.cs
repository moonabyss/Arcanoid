using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour {

	// Use this for initialization
	void OnEnable ()
    {
        transform.Rotate(0, 0, Random.Range(-3, 3));
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
