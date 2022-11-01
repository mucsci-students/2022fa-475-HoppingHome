using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlatformerCharacter2D character;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            character = GameObject.Find("Ziggy").GetComponent<PlatformerCharacter2D>();
        }
        totalHealthBar.fillAmount = character.health / 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = character.health / 10.0f;
    }
}
