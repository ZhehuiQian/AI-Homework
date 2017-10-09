using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Transform player;
    public float SteeringForce=5.0f;
    Vector3 Velocity;

    // for wander
    Vector3 CircleCenter;
    Vector3 offset;
    float RandomAngel;

    // for seek and arrival
    Vector3 Target;
    bool FoundFood;
    public float slowingRadius;
    

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Cow").transform;
        InvokeRepeating("SetAngel", 0f, 0.5f);
        InvokeRepeating("SetOffset", 0f, 0.5f);
        FoundFood = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
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
        CircleCenter = player.position + offset;
        float CircleRadius = 4.0f;
        Vector3 RandomVector = new Vector3(Mathf.Cos(RandomAngel)*CircleRadius, 0, Mathf.Sin(RandomAngel)*CircleRadius);
        //print(RandomVector);
        Velocity = Vector3.Normalize(CircleCenter + RandomVector)*SteeringForce;
        //print("velocity:"+Velocity+",CircleCenter:"+CircleCenter);
        player.position += Velocity * Time.deltaTime;
    }

    void SetAngel()
    {
        //offset = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        RandomAngel = Random.Range(0, Mathf.PI * 2);
    }

    void SetOffset()
    {
        offset = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        print("offset" + offset);
    }

    void Seek()
    {
        //print("seek!");
        // heading towards the location of the target, fake arriving behavior
        float distance = Vector3.Distance(Target, player.position);
        Velocity = Vector3.Normalize(Target - player.position) * SteeringForce*distance/slowingRadius;
        //print(Velocity);
        player.position += Velocity * Time.deltaTime;
        if(Vector3.Distance(player.position,Target)<=0.1)
        {
            FoundFood = false;
        }
       
    }
}
