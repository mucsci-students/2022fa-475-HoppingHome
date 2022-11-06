using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class FireRateBuffScript : MonoBehaviour
{
    public GameObject player;
    public Platformer2DUserControl character;
    public float fireRateMult = 2.0f;
    public float duration = 5.0f;
    private float startDuration;
    private bool isActivated = false;
    private float orginalCooldown;

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
            character = player.GetComponent<Platformer2DUserControl>();
        }
        if (totalBar == null)
        {
            totalBar = GameObject.Find("fireRateMaxImg").GetComponent<Image>();
        }
        if (currentBar == null)
        {
            currentBar = GameObject.Find("fireRateCurrImg").GetComponent<Image>();
        }
        startDuration = duration;
        orginalCooldown = character.bulletCooldown;
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
                character.bulletCooldown = orginalCooldown;
                totalBar.gameObject.SetActive(false);
                currentBar.gameObject.SetActive(false);
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
            totalBar.gameObject.SetActive(true);
            currentBar.gameObject.SetActive(true);
        }
    }

    public void respawn()
    {
        Debug.Log("Inside Respawn");
        duration = startDuration;
        isActivated = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
