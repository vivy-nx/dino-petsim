using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Dinosaur : MonoBehaviour
{
    public enum species
    {
        unknown,
        spinosaurus,
        brontosaurus,
        triceratops,
        stegasaurus,
        baronyx,
        halzkaraptor
        // add more as needed
    }
    public enum behavior
    {
        idle,
        looking,
        moving,
        eating,
        emoting,
        sleeping,
    }
    bool enteringNewState = true;
    public species dinoSpecies = species.unknown;
    public string dinoName = "Dinosaur";
    public float age = 0f;
    public float hunger = 0f;
    public float health = 100f;
    public float happiness = 10f; //unhappiness negative? low energy unhappiness sadness and high energy unhappiness anger?
    public int energy = 100;
    public behavior behaviorState = behavior.idle;
    float behaviorTimer = 0; //determine how long a dinosaur will do behaviors for, sometimes
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        randomTimerLength();
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        hunger += Time.deltaTime / 20;
        behaviorTimer -= Time.deltaTime;

        //state machine
        stateMachine();
        
    }
    void stateMachine()
    {
        if (enteringNewState == true) //start of new state 
        {
            enteringNewState = false;
            switch (behaviorState)
            {
                case behavior.idle:
                    //Debug.Log(dinoName + " idling");
                    randomTimerLength();
                    break;
                case behavior.looking:
                    randomTimerLength();
                    agent.SetDestination(
                        transform.position + new Vector3(
                            UnityEngine.Random.Range(-1, 1),
                            0,
                            UnityEngine.Random.Range(-1, 1)
                        )
                    );
                    break;
                case behavior.moving:
                    //Debug.Log(dinoName + " moving");
                    //set destination to a random point on XZ
                    float magnitude = UnityEngine.Random.Range(10, 30);
                    agent.SetDestination(
                        transform.position + new Vector3(
                            UnityEngine.Random.Range(-magnitude, magnitude), 
                            0, 
                            UnityEngine.Random.Range(-magnitude, magnitude)
                        )
                    );
                    break;
                case behavior.eating:

                    break;
                case behavior.emoting:

                    break;
                case behavior.sleeping:

                    break;
            }
        }
        else //update for current state (maybe not all that much needs to be done on a per frame basis.
        {
            switch (behaviorState)
            {
                case behavior.idle:
                    if (behaviorTimer < 0)
                    {
                        //will probably want to make a more complex system or get a random roll for what states are entered from idle timer ending
                        //but for now only moving is implemented so. 
                        ChangeState(behavior.moving);
                    }
                    break;
                case behavior.moving:
                    if (agent.remainingDistance < 0.2)
                    {
                        bool coinflip = (UnityEngine.Random.value <= 0.5);
                        if (coinflip)
                        {
                            ChangeState(behavior.moving);
                        } else
                        {
                            ChangeState(behavior.idle);
                        }
                    }
                    break;
                
            }
        }
    }
    void ChangeState(behavior newState)
    {
        behaviorState = newState;
        enteringNewState = true;
    }

    void randomTimerLength()
    {
        behaviorTimer = UnityEngine.Random.Range(3,15);
    }
}