using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {

    //GameObject player;
    Rigidbody playerRig;
    Vector3 Target;

	// Use this for initialization
	void Start () {
        playerRig = GetComponent<Rigidbody>();
        Target = GameObject.FindGameObjectWithTag("Food").transform.position;
	}


    // Update is called once per frame
    void Update () {
        Vector3 SteeringDirection = Vector3.Normalize(Target - playerRig.position);
        playerRig.AddForce(SteeringDirection * 5.0f);
    }

    void FixedUpdate()
    {
        //Vector3 SteeringDirection = Vector3.Normalize(Target - playerRig.position);
        //playerRig.AddForce(SteeringDirection*5.0f);
    }
}
