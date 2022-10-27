using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class FireRateBuffScript : MonoBehaviour
{
    public GameObject player;
    public Platformer2DUserControl character;
    public float fireRateMult = 2.0f;
    public float duration = 5.0f;
    private bool isActivated = false;
    private float orginalCooldown;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Ziggy");
        }
        if (character == null)
        {
            character = player.GetComponent<Platformer2DUserControl>();
        }
        orginalCooldown = character.bulletCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated) 
        {
            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                character.bulletCooldown = orginalCooldown;
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isActivated && col.gameObject.name == "Ziggy")
        {
            character.bulletCooldown = orginalCooldown / fireRateMult;
            isActivated = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}