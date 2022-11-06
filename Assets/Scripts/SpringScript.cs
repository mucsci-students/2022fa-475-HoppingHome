using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour
{

    public float jumpForce = 20f;
    private AudioSource source;

    void OnTriggerEnter2D(Collider2D collision)
    {
        source = GetComponent<AudioSource>();
        source.Play();
        if (collision.gameObject.name == "Ziggy")
        {
            Debug.Log("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
        }
    }
}
