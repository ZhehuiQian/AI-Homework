using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cow")
        {
            //Destroy(gameObject, 1.0f);

        }
    }

}
