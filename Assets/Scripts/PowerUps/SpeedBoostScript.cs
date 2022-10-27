using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class SpeedBoostScript : MonoBehaviour
{
    public GameObject player;
    public PlatformerCharacter2D character;
    public float speedMult = 1.5f;
    public float duration = 5.0f;
    private bool isActivated = false;
    private float orginalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Ziggy");
        }
        if (character == null)
        {
            character = player.GetComponent<PlatformerCharacter2D>();
        }
        orginalSpeed = character.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated) 
        {
            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                character.m_MaxSpeed = orginalSpeed;
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isActivated && col.gameObject.name == "Ziggy")
        {
            character.m_MaxSpeed = orginalSpeed * speedMult;
            isActivated = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
