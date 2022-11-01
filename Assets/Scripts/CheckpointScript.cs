using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CheckpointScript : MonoBehaviour
{
    private PlatformerCharacter2D character;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            character = GameObject.Find("Ziggy").GetComponent<PlatformerCharacter2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ziggy")
        {
            character.m_spawn = transform;
            Debug.Log(character.m_spawn);
        }
    }
}
