using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class SpeedBoostScript : MonoBehaviour
{
    public GameObject player;
    public PlatformerCharacter2D character;
    public float speedMult = 1.5f;
    public float duration = 5.0f;
    private float startDuration;
    private bool isActivated = false;
    private float orginalSpeed;

    [SerializeField] private Image totalBar;
    [SerializeField] private Image currentBar;

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
        if (totalBar == null)
        {
            totalBar = GameObject.Find("SpeedBoostMaxImg").GetComponent<Image>();
        }
        if (currentBar == null)
        {
            currentBar = GameObject.Find("SpeedBoostCurrImg").GetComponent<Image>();
        }
        startDuration = duration;
        orginalSpeed = character.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated) 
        {
            currentBar.fillAmount = duration / startDuration;
            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                character.m_MaxSpeed = orginalSpeed;
                totalBar.gameObject.SetActive(false);
                currentBar.gameObject.SetActive(false);
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
            totalBar.gameObject.SetActive(true);
            currentBar.gameObject.SetActive(true);
        }
    }

    public void respawn()
    {
        duration = startDuration;
        isActivated = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
