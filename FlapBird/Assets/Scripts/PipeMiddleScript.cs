using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles pipe behavior and player interaction
public class PipeMiddleScript : MonoBehaviour
{
    public LogicScript logic; // Ref to the LogicScript that manages the score
    
    void Start()
    {
        // Get the LogicScript from the "Logic" GameObject.
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>(); 
    }

    void Update()
    {
        
    }

    // Called when another collider enters this trigger.
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 3) 
        {
            // If condition is met, add score
            logic.addScore(1);
        }
    }
}
