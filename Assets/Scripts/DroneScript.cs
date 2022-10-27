using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{

    public float speed = 3f;
    public int health = 3;
    public bool isRight = false;
    public Transform leftBound;
    public Transform rightBound;
    private Transform start;
    private bool angered = false;
    private GameObject player;
    private Rigidbody2D rb2D;

    void Start()
    {
        start = transform;
        rb2D = GetComponent<Rigidbody2D>();
        leftBound.position = transform.position + leftBound.position;
        rightBound.position = transform.position + rightBound.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!angered)
        //{
            if ((isRight && transform.position.x > rightBound.position.x) || 
                (!isRight && transform.position.x < leftBound.position.x))
            {
                flip();
            }
            // Move the character
            rb2D.velocity = new Vector2(speed, 0);
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ziggy")
        {
            angered = true;
            player = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Ziggy")
        {
            angered = false;
        }
    }

    private void flip()
    {
        // Face diff direction
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isRight = !isRight;
    }
}
