using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubsumption : MonoBehaviour {

    public PlayerSensory _playerSensory;
    public PlayerMovement1 _playerMovement;

    private bool FoundFood;
    private bool FoundEnemy;
    private float Health;

    // parameters to pass to movement
    public string Instruction;
    public Vector3 Target;

    // parameters to get from the movement
    public bool AteFood;

    void Start()
    {
        FoundFood = false;
        FoundEnemy = false;
        AteFood = _playerMovement.AteFood;
        Target = Vector3.zero;
    }

    void Update()
    {
        AteFood = _playerMovement.AteFood;

        FoundFood = _playerSensory.FoundFood;
        FoundEnemy = _playerSensory.FoundEnemy;
        Health = _playerSensory.HealthPoint;
        Target = _playerSensory.Target;
        if (FoundFood&&!AteFood)
        {
            if (FoundEnemy)
            {
                if (Health > 70)
                { Instruction = "Attack"; }
                else Instruction = "Flee";
            }
            else Instruction = "Eat";
        }
        else Instruction = "Wander";
    }


}
