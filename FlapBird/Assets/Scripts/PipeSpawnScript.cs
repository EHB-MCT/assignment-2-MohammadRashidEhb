using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles spawning pipes at regular intervals and random heights
public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe; // Pipe prefab to spawn 
    public float spawnRate = 2; // How often pipes spawn
    private float timer = 0; // Timer to track next pipe spawn
    public float heightOffset = 10; // Range for randomizing pipe heights

    void Start()
    {
        spawnPipe();
    }

    void Update()
    {
        if (timer < spawnRate) 
        {
            // Adds time since the last frame to the timer.
            timer = timer + Time.deltaTime;
        }
        else
        {
            // Spawn new pipe and reset timer
            spawnPipe();
            timer = 0;
        }
    }
    
    // Spawn pipe at random height within heightOffset
    void spawnPipe() 
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
