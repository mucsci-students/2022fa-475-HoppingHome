using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawnScript : MonoBehaviour
{
    private GameObject player;
    public float maxDistance = 20f;

    void Start()
    {
        player = GameObject.Find("Ziggy");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bulletPos = transform.position;
        float curDistance = Vector3.Distance(bulletPos, player.transform.position);
        if (curDistance > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
