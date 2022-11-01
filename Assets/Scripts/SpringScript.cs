using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour
{

    public float jumpForce = 20f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ziggy")
        {
            Debug.Log("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
        }
    }
}
