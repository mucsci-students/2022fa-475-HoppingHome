using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class HealthPickUpScript : MonoBehaviour
{
    public PlatformerCharacter2D character;
    public int healAmount = 1;
    private int maxHealth;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (character == null)
        {
            character = GameObject.Find("Ziggy").GetComponent<PlatformerCharacter2D>();
        }
        maxHealth = character.health;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ziggy" && character.health < maxHealth)
        {
            source.Play();
            character.health += healAmount;
            if (character.health > maxHealth)
                character.health = maxHealth;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void respawn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
