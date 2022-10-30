using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawnScript : MonoBehaviour
{
    private GameObject player;
    public float maxDistance = 20f;
    public float maxTime = 20f;
    private float timer = 0f;

    void Start()
    {
        player = GameObject.Find("Ziggy");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bulletPos = transform.position;
        float curDistance = Vector3.Distance(bulletPos, player.transform.position);
        if (curDistance > maxDistance || timer > maxTime)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
