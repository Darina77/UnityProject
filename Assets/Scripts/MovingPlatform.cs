using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Vector3 MoveBy;
    public float waitTime = 2.0f;
    public float speed = 0.2f;
    float time_to_wait;
    Vector3 pointA;
    Vector3 pointB;
    bool going_to_a;
    // Use this for initialization
    void Start () {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        this.going_to_a = false;
        this.time_to_wait = -2.0f;

    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.2f;
    }

    // Update is called once per frame
    void FixedUpdate () {
        time_to_wait -= Time.deltaTime;
        if (time_to_wait <= 0)
        {

            Vector3 my_pos = this.transform.position;
            Vector3 target;
            if (going_to_a)
            {
                target = this.pointA;

            }
            else
            {
                target = this.pointB;

            }
            if (isArrived(my_pos, target))
            {
                going_to_a = !going_to_a;
                time_to_wait = waitTime;
            }
            else
            {
                Vector3 destination = target - my_pos;
                destination.z = 0;
                float magnitude = destination.magnitude;
                if (magnitude <= speed || magnitude == 0f)
                {
                    my_pos = target;
                }
                else my_pos += destination / magnitude * speed;
                this.transform.position = my_pos;
            }
        }
        else { return; }
    }
}
