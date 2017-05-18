using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {
    public float speed = 1;
    Rigidbody2D myBody = null;
    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        //[-1, 1]
        float value = Input.GetAxis("Horizontal");

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }
    }
}
