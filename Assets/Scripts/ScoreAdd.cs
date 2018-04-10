using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAdd : MonoBehaviour {

    public float lifeTime = 2f;
    public Text scoreText;
    public int score;

    private float currentLifeTime;

    // Use this for initialization
    void Enable ()
    {
        currentLifeTime = Time.time;
        scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentLifeTime += Time.deltaTime;
        gameObject.transform.position += new Vector3(0, 0.5f, 0) * Time.deltaTime;
        if (currentLifeTime > lifeTime)
        {
            Destroy(gameObject);
        }
	}
}
