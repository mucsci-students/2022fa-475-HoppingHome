using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawnScript : MonoBehaviour
{
    private GameObject player;
    public float maxDistance = 15f;

    void Start()
    {
        Debug.Log("bullet spawn");
        player = GameObject.Find("Ziggy");
        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bulletPos = transform.position;
        Debug.Log(Vector3.Distance(bulletPos, player.transform.position));
        if (Vector3.Distance(bulletPos, player.transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
