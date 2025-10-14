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
    public species dinoSpecies = species.unknown;
    public string dinoName = "Dinosaur";
    public float age = 0f;
    public float hunger = 0f;
    public float health = 100f;
    public float happiness = 10f; //unhappiness negative? low energy unhappiness sadness and high energy unhappiness anger?
    public int energy = 100;
    public behavior behaviorState = behavior.idle;
    public float behaviorTimer = 0; //determine how long a dinosaur will do behaviors for, sometimes

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        hunger += Time.deltaTime / 20;

        //control what is done every frame when state updates. probably can be broken down into smaller functions as needed but for now it can just be like this
        switch(behaviorState)
        {
            case behavior.idle:
            
                break;
            case behavior.looking:

                break;
            case behavior.moving:
                
                break;
            case behavior.eating:

                break;
            case behavior.emoting:

                break;
            case behavior.sleeping:

                break;
        }
    }
}