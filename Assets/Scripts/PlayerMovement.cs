using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Transform player;
    public float SteeringForce;
    Vector3 Velocity;
    Vector3 CircleCenter;
    Vector3 offset;
    Vector3 Target;
    bool FoundFood;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Cow").transform;
        InvokeRepeating("Setoffset", 0f, 1.0f);
        FoundFood = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag=="Food")
        {
            FoundFood = true;
            Target = col.transform.position;
        }

    }
    // Update is called once per frame
    void Update () {

        
        if (FoundFood == false)
        {// player wander around
            Wander();
        }
        else
        {
            Seek();
        }
    }

    void Wander()
    {
        //Setoffset();
        CircleCenter = player.position + offset;
        Vector3 RandomVector = new Vector3(Random.Range(-4.0f,4.0f), 0, Random.Range(-4.0f, 4.0f));
        //print(RandomVector);
        Velocity = Vector3.Normalize(CircleCenter + RandomVector)*SteeringForce;
        print("velocity:"+Velocity+",CircleCenter:"+CircleCenter);
        player.position += Velocity * Time.deltaTime;
    }

    void Setoffset()
    {
        offset = new Vector3(Random.Range(-10.0f,10.0f), 0, Random.Range(-10.0f, 10.0f));
        print("offset"+offset);
    }

    void Seek()
    {
        //print("seek!");
        Velocity= Vector3.Normalize(Target - player.position)*SteeringForce;
        player.position += Velocity * Time.deltaTime;
        if (player.position == Target)
        {
            Velocity=Vector3.zero;
            //FoundFood = false;

        }
    }
}
