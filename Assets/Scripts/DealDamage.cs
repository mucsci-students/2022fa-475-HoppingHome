using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class DealDamage : MonoBehaviour
{   
    [SerializeField] int damage;
    private PlatformerCharacter2D character;

    void Start()
    {
        if (character == null)
        {
            character = GameObject.Find("Ziggy").GetComponent<PlatformerCharacter2D>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.name == "Ziggy")
        {
           character.health -= damage;
            
        }
    }
}
