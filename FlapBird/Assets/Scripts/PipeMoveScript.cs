using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the movement of pipes in the game
public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5; // Speed at which the pipe moves
    public float deadZone = -45; // Position threshold to delete the pipe

    void Start()
    {
    }

    void Update()
    {
        // Moves the pipe to the left based on moveSpeed
        // It adjusts for frame rate by multiplying with Time.deltaTime.
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        // If the pipe moves past the deadZone, delete the pipe
        if(transform.position.x < deadZone) 
        {
            Destroy(gameObject);
        }
    }
}
