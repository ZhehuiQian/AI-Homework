using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubsumption : MonoBehaviour {

    public PlayerSensory _playerSensory;
    public PlayerMovement1 _playerMovement;

    private bool FoundFood;
    private bool FoundEnemy;


    // parameters to pass to movement
    public string Instruction;
    public Vector3 Target;
    public float Health;

    // parameters to get from the movement
    //public bool AteFood;

    void Start()
    {
        FoundFood = false;
        FoundEnemy = false;
        Target = Vector3.zero;
        //Health = 100f;
    }

    void Update()
    {
        FoundFood = _playerSensory.FoundFood;
        FoundEnemy = _playerSensory.FoundEnemy;
        print("FoundEnemy?" + FoundEnemy);
        if (Health > 70)
        {
            if (FoundFood && !FoundEnemy && !_playerMovement.AteFood)
            {
                Instruction = "Eat";
                Target = _playerSensory.FoodPos;
            }
            else if (FoundFood && FoundEnemy)
            {
                Instruction = "Attack";
                Target = _playerSensory.EnemyPos;
            }
            else Instruction = "Wander";
           
        }

        else if (Health > 20 && Health<70)
        {
            if (FoundFood && !FoundEnemy && !_playerMovement.AteFood)
            {
                Instruction = "Eat";
                Target = _playerSensory.FoodPos;
            }
            else Instruction = "Wander";
        }

       else if (Health > 0 && Health<20)
        {
            if (FoundFood && !FoundEnemy && !_playerMovement.AteFood)
            {
                Instruction = "Eat";
                Target = _playerSensory.FoodPos;
            }
            else if (FoundFood && FoundEnemy)
            {
                Instruction = "Flee";
                Target = _playerSensory.EnemyPos;
            }
            else Instruction = "Wander";
         
        }

       else
        {
            Instruction = "Die";
        }


        // old stuff
        /*  if (FoundFood&&!AteFood)
          {
              if (FoundEnemy)
              {
                  if (Health > 70)
                  {
                      Instruction = "Attack";
                      Health -= Time.deltaTime * 10.0f;
                      print("Health" + Health);
                  }
                  else Instruction = "Flee";
              }
              else Instruction = "Eat";
          }
          else Instruction = "Wander";
          */
    }


}
