using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public int score = 5;
    public int durability = 1;

    private Animator anim;

    public delegate void CellDestroyed(int score, Transform position);
    public static event CellDestroyed OnCellDestroyed;
    public AudioClip audioClipMain;
    public AudioClip audioClipAlt;
    [Range(0f, 1f)]
    public float volume = 1f;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
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
            if (durability > 0 && anim)
            {
                anim.SetTrigger("blink");
            }
            if (durability == 0)
            {
                OnCellDestroyed(score, transform);
                Destroy(gameObject);
            }
        }
    }

    public AudioClip GetAudioClip()
    {
        if (durability > 1)
        {
            return audioClipAlt;
        }
        else
        {
            return audioClipMain;
        }
    }
}
