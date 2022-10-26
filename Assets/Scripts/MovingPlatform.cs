using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MovingPlatform : MonoBehaviour
{
    public float speed; // speed of the platform
    public int startingPoint; // starting index (pos of platform)
    public Transform[] points; // array of transform points (pos where the platform needs to move)
    public PlatformerCharacter2D character; // Link to character script
    public bool playerOn = false;
    public bool facingRight = false;

    private int i; // index of the array

    void Start()
    {   
        // setting the pos of the platform to the pos of one of the
        // starting points using the index "startingPoint"
        transform.position = points[startingPoint].position;
        // Slower, set character through editor when possible, use this as backup.
        if (character == null)
        {
            character = GameObject.Find("Ziggy").GetComponent<PlatformerCharacter2D>();
        }
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
            facingRight = !facingRight;
            if (i == points.Length)
            {
                i = 0;
            }
            if (playerOn)
            {
                if (character.m_FacingRight == facingRight)
                {
                    character.Flip();
                }
                character.m_FacingRight = !character.m_FacingRight;
            }
        }

        // moving the platform to the point pos with index "i"
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
        if (collision.gameObject.name == "Ziggy")
        {
            playerOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
        if (collision.gameObject.name == "Ziggy")
        {
            playerOn = false;
        } 
    }
}
