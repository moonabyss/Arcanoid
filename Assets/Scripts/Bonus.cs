using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    // Use this for initialization
    //private float currentLifeTime;

    // Use this for initialization
    void Enable()
    {
        //currentLifeTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0, -2f, 0) * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Floor")
        {
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Player" && GameManager.GetGameMode() == GameMode.PLAYING)
        {
            Action();
        }
    }

    protected virtual void Action()
    {

    }
}
