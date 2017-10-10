using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour {

    // for receiving info from the subsumption
    public PlayerSubsumption _playersubsumption;
    private string Instruction;

    Transform player;
    public float SteeringForce=5.0f;
    Vector3 Velocity;

    // for wander
    Vector3 CircleCenter;
    Vector3 offset;
    float RandomAngel;

    // for seek and arrival
    Vector3 Target;
    public float slowingRadius;

    // infos to pass to the subsumption
    //public bool FinishAttack;
    //public bool FinishFlee;
    public bool AteFood;
    //public float Health;


    // Use this for initialization
    void Start () {
        player = GetComponent<Rigidbody>().transform;
        InvokeRepeating("SetAngel", 0f, 0.5f);
        InvokeRepeating("SetOffset", 0f, 0.5f);
        AteFood = false;
        Instruction = _playersubsumption.Instruction;
        Target = _playersubsumption.Target;
    }

    // Update is called once per frame
    void Update () {

        Instruction = _playersubsumption.Instruction;
        print("Instruction:" + _playersubsumption.Instruction);
        if (Instruction == "Eat")
        {
            Target = _playersubsumption.Target;
            Seek();
        }
        else if (Instruction == "Attack")
        {
            Target = _playersubsumption.Target;
            Attack();
        }
        else if (Instruction == "Flee")
        {
            Target = _playersubsumption.Target;
            Flee();
        }

        else if(Instruction== "Die")
        {
            Die();
        }

        else if(Instruction== "Wander")
        {
            Wander();
        }

        else { print("no instructions"); }
        
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
        //print("wander");
    }

    void SetAngel()
    {
        //offset = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        RandomAngel = Random.Range(0, Mathf.PI * 2);
    }

    void SetOffset()
    {
        offset = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        //print("offset" + offset);
    }

    void Seek()
    {
       
        // heading towards the location of the target, fake arriving behavior
        float distance = Vector3.Distance(Target, player.position);
        Velocity = Vector3.Normalize(Target - player.position) * SteeringForce*distance/slowingRadius;
        //print(Velocity);
        player.position += Velocity * Time.deltaTime;
        if(Vector3.Distance(player.position,Target)<=0.1)
        {

            AteFood = true;
            // eat can get health recover
            if (_playersubsumption.Health < 90)
                _playersubsumption.Health += 10;
            else if (_playersubsumption.Health < 100)
                _playersubsumption.Health = 100;
           
        }
        
       
    }

    void Attack()
    {
        float timer = Time.deltaTime;
        float distance = Vector3.Distance(Target, player.position);
        Velocity = Vector3.Normalize(Target - player.position) * SteeringForce * distance / slowingRadius;
        player.position += Velocity * Time.deltaTime;

        // health drops 5 when attack each other

            _playersubsumption.Health -= Time.deltaTime * 10.0f;



    }

    void Flee()
    {
        float timer = Time.deltaTime;
        Velocity = Vector3.Normalize(player.position - Target) * SteeringForce;
        player.position += Velocity * Time.deltaTime;

    }

    void Die()
    {
        Destroy(gameObject, 1.0f);
    }
}
