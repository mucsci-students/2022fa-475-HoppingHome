using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public bool moving = false;
    public float velocityX = -5f;
    public float spawnDistance = 50f;
    public float despawnDistance = 100f;
    private Vector3 start;
    public GameObject player;
    public GameObject carPrefab;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
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
            GetComponent<SpriteRenderer>().enabled = true;
            moving = true;
        } else if (moving && curDistance > despawnDistance)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            moving = false;
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

    public void respawn()
    {
        GameObject car = Instantiate(carPrefab, start, Quaternion.identity);
        CarScript cs = car.GetComponent<CarScript>();
        cs.moving = false;
        cs.velocityX = velocityX;
        cs.spawnDistance = spawnDistance;
        cs.despawnDistance = despawnDistance;
        cs.player = player;
        cs.GetComponent<SpriteRenderer>().enabled = true;
    }
}
