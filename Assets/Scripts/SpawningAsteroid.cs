using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningAsteroid : MonoBehaviour
{

    private float timer = 0f;
    public float spawnCooldown = 1.00f;
    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
       if (timer > spawnCooldown)
            {
                spawn();
                timer = 0f;
            }

            timer += Time.deltaTime; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        GameObject temp = Instantiate(asteroid, transform.position, Quaternion.identity);
    }
}
