using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public bool moving = false;
    public float velocityX = -5f;
    public float spawnDistance = 50f;
    public float despawnDistance = 100f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Ziggy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float curDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!moving && curDistance < spawnDistance)
        {
            moving = true;
        } else if (moving && curDistance > despawnDistance)
        {
            Destroy(gameObject);
        }


        if (moving)
        {
            transform.position += new Vector3(velocityX, 0, 0) * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ziggy")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 500f));
        }
    }
}
