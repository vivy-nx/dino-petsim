using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public float happiness = 10f; // unhappiness negative? low energy unhappiness sadness and high energy unhappiness anger?
    public int energy = 100;
    public behavior behaviorState = behavior.idle;
    public float behaviorTimer = 0; // determine how long a dinosaur will do behaviors for, sometimes
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomTimerLength();
        // Initialize variables if necessary, but health and hunger should already be set
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        hunger += Time.deltaTime / 20;
        behaviorTimer -= Time.deltaTime;
        HandleHealthLoss();
        //state machine
        stateMachine();
        // clamp functions ( i explain it later i just added this last )
        health = Mathf.Clamp(health, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        energy = Mathf.Clamp(energy, 0, 100);
    }
    void stateMachine()
    {
        if (enteringNewState == true) //start of new state 
        {
            enteringNewState = false;
            switch (behaviorState)
            {
                case behavior.idle:
                    HandleIdleBehavior();
                    break;
                case behavior.looking:
                    HandleLookingBehavior();
                    break;
                case behavior.moving:
                    HandleMovingBehavior();
                    break;
                case behavior.eating:
                    HandleEatingBehavior();
                    break;
                case behavior.emoting:
                    HandleEmotingBehavior();
                    break;
                case behavior.sleeping:
                    HandleSleepingBehavior();
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
                        }
                        else
                        {
                            ChangeState(behavior.idle);
                        }
                    }
                    break;

            }
        }


    }

    //--------------------------------------------------
    // handle behavior here
    //--------------------------------------------------

    private void HandleIdleBehavior()
    {
        randomTimerLength();
        // increase hunger and decrease happiness while dino idle
        hunger += Time.deltaTime / 10; // hunger increase 
        happiness -= Time.deltaTime / 30; // decrease happiness
    }

    private void HandleLookingBehavior()
    {
        randomTimerLength();
        agent.SetDestination(
            transform.position + new Vector3(
                UnityEngine.Random.Range(-1, 1),
                0,
                UnityEngine.Random.Range(-1, 1)
            )
        );
    }

    private void HandleMovingBehavior()
    {
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
    }

    private void HandleEatingBehavior()
    {
        hunger -= Time.deltaTime * 5; // decrease hunger when eating
        if (hunger <= 0f)
        {
            hunger = 0f;
            behaviorState = behavior.idle; // return dino to idle state after eating
        }
    }

    private void HandleEmotingBehavior()
    {
  
    }

    private void HandleSleepingBehavior()
    {
        // restore energy when sleeping
        energy += (int)(Time.deltaTime * 2); // restore energy (had to cast to an int)
        if (energy >= 100)
        {
            energy = 100;
            behaviorState = behavior.idle; // return to idle
        }
    }

    private void HandleHealthLoss()
    {
        if (hunger <= 0f)
        {
            health -= 5; // lose health due to starvation
        }
        if (happiness <= 0f)
        {
            health -= 5; // lose health due to unhappiness
        }
    }

    public void Pet()
    {
        if (hunger > 50) // when dinos are hungry, they dont respond as well
        {
            happiness += 5; // hungry +5
            health += 3;  // gains small bit of health from the power of love
        }
        else
        {
            happiness += 10; // happy dino +10
            health += 5;  // gains more health because it's not hungry
        }

        happiness = Mathf.Clamp(happiness, 0f, 100f); // this makes sure that the happiness stays in bounds
        health = Mathf.Clamp(health, 0f, 100f); // clamping health to ensure it stays between 0 and 100
    }

    public void Feed()
    {
        hunger -= 20f; // decrease hunger
        if (hunger < 0f) hunger = 0f; // make sure that hunger does not go below zero

        energy += 10; // feeding gives energy as well (we can change this)
        health += 25; // feeding gives health
        energy = Mathf.Clamp(energy, 0, 100); // clamp to max energy value
        health = Mathf.Clamp(health, 0f, 100f); // clamp health value
    }

    public void SetName(string newName)  // if we want to change the dino's name
    {
        dinoName = newName;
    }

    void ChangeState(behavior newState)
    {
        behaviorState = newState;
        enteringNewState = true;
    }

    void randomTimerLength()
    {
        behaviorTimer = UnityEngine.Random.Range(3, 15);
    }
}
