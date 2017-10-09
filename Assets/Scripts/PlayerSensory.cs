using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensory : MonoBehaviour {
    // parameters get from the player subsumption


    // parameters pass to the player subsumption
    public bool FoundFood;
    public bool FoundEnemy;
    public Vector3 FoodPos;
    public Vector3 EnemyPos;
    public float range;

    // private stuff
    GameObject[] Enemys;
       
    void Start()
    {
        FoundFood = false;
        FoundEnemy = false;
        Enemys = GameObject.FindGameObjectsWithTag("Cow");
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.tag == "Food")
        {
            FoundFood = true;
            FoodPos = col.transform.position;
         
            foreach (GameObject Enemy in Enemys)
            {
               
                // check if there is a potential goal
                float dis_food = Vector3.Distance(FoodPos, Enemy.transform.position);
                if (dis_food <=range && Enemy!=gameObject)
                {
                    print("dis_food" + dis_food);
                    FoundEnemy = true;
                    EnemyPos = Enemy.transform.position;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.tag == "Food")
        {
            FoundFood = false;
            FoundEnemy = false;
        }
            
    }

    void Update()
    {
     
        //    //HealthPoint = _playersubsumption.Health;
        //    if (FoundFood)
        //    {
        //        if (FoundEnemy)
        //        {
        //            Target = EnemyPos;

        //        }

        //        else Target = FoodPos;
        //        //print("passTarget");
        //    }
        //    else Target = Vector3.zero;
    }
}
