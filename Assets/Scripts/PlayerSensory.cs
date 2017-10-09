using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensory : MonoBehaviour {

    public bool FoundFood;
    public bool FoundEnemy;
    private Vector3 FoodPos;
    private Vector3 EnemyPos;
    GameObject[] Enemys;
    public Vector3 Target;
    public float HealthPoint;
   
    void Start()
    {
        FoundFood = false;
        FoundEnemy = false;
        Enemys = GameObject.FindGameObjectsWithTag("Cow");
        HealthPoint = 100.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.tag == "Food")
        {
            FoundFood = true;
            FoodPos = col.transform.position;
            //print("found food!");
            foreach (GameObject Enemy in Enemys)
            {
                print("FoundEnemy");
                // check if there is a potential goal
                float dis_food = Vector3.Distance(FoodPos, Enemy.transform.position);
                if (dis_food <2.0f)
                {
                    FoundEnemy = true;
                    EnemyPos = Enemy.transform.position;
                }
            }
        }

    }

    void Update()
    {
        if (FoundFood)
        {
            if (FoundEnemy)
            {
                Target = EnemyPos;
            }

            else Target = FoodPos;
            print("passTarget");
        }
        else Target = Vector3.zero;
    }
}
