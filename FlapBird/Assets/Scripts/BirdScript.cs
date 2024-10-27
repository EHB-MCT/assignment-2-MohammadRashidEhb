using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the bird's movement when the player presses the spacebar
public class BirdScript : MonoBehaviour
{
    // Reference to the bird's Rigidbody2D component, which handles the physics (like jumping)
    public Rigidbody2D myRigidbody;

    public float flapStrength;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // This checks if the spacebar is pressed 
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            // This applies an upward force to the bird, making it jump
             myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }
}
