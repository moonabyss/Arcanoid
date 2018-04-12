using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb;
    private AudioSource audioPlayer;
    public GameObject soundPrefab;

    public Vector2 BallVelocity { get; private set;  }

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        audioPlayer = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void FixedUpdate()
    {
        if (rb.velocity != Vector2.zero)
        {
            BallVelocity = rb.velocity;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Cell")
        {
            /*
            audioPlayer.clip = collision.collider.GetComponent<Cell>().GetAudioClip();
            audioPlayer.volume = collision.collider.GetComponent<Cell>().volume;
            audioPlayer.Play();
            */
            GameObject sound = Instantiate(soundPrefab, transform);
            sound.GetComponent<AudioSource>().clip = collision.collider.GetComponent<Cell>().GetAudioClip();
            //sound.GetComponent<AudioSource>().volume = collision.collider.GetComponent<Cell>().volume;
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
        }
        else if (collision.collider.tag == "Player")
        {
            /*
            audioPlayer.clip = collision.collider.GetComponent<Carete>().GetAudioClip();
            audioPlayer.volume = collision.collider.GetComponent<Carete>().volume;
            audioPlayer.Play();
            */
            GameObject sound = Instantiate(soundPrefab, transform);
            sound.GetComponent<AudioSource>().clip = collision.collider.GetComponent<Carete>().GetAudioClip();
            //sound.GetComponent<AudioSource>().volume = collision.collider.GetComponent<Carete>().volume;
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
        }
    }

}
