using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class HealthPickUpScript : MonoBehaviour
{
    public PlatformerCharacter2D character;
    public int healAmount = 1;
    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
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
            character.health += healAmount;
            if (character.health > maxHealth)
                character.health = maxHealth;
            gameObject.SetActive(false);
        }
    }
}
