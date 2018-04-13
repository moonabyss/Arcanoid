using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carete : MonoBehaviour {
    public GameObject ball;
    public AudioClip audioClipMain;
    public AudioClip audioClipLaunch;
    public GameObject soundPrefab;

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
            if (GameManager.GetGameMode() == GameMode.PLAYING && rb.velocity == Vector2.zero)
            {
                float angle = Random.Range(60f, 120f);
                ball.transform.SetParent(null);
                // Cursor position to angle. Angle to vector2
                rb.AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * thrust);

                GameObject sound = Instantiate(soundPrefab, transform);
                sound.GetComponent<AudioSource>().clip = audioClipLaunch;
                sound.GetComponent<AudioSource>().Play();
                Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
            } 
        }
    }

    public AudioClip GetAudioClip()
    {
        return audioClipMain;
    }

}
