using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; // speed of the platform
    public int startingPoint; // starting index (pos of platform)
    public Transform[] points; // array of transform points (pos where the platform needs to move)

    private int i; // index of the array

    void Start()
    {   
        // setting the pos of the platform to the pos of one of the
        // starting points using the index "startingPoint"
        transform.position = points[startingPoint].position;
    }


    void FixedUpdate()
    {
        // Checking the distance of the paltform and the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            // Face diff direction
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        // moving the platform to the point pos with index "i"
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
