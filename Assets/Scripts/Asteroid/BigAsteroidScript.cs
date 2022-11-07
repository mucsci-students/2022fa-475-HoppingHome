using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAsteroidScript : MonoBehaviour
{

    public Vector2 fallSpeed = new Vector2(0, -5);
    public float xOffsetSpawnDistance = 10f;
    public float despawnDistance = 100f;
    public bool isFalling = false;
    public GameObject player;
    private Animator anim;
    private Vector3 start;
    private float respawnDelay = 1.0f;
    private float timer = 0f;
    public GameObject asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        if (player == null)
        {
            player = GameObject.Find("Ziggy");
        }
        anim = GetComponent<Animator>();
        isFalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > respawnDelay)
        {
            if (isFalling)
            {
                Vector3 asteroidPos = transform.position;
                float curDistance = Vector3.Distance(asteroidPos, player.transform.position);
                if (curDistance > despawnDistance)
                {
                    //Destroy(gameObject);
                }
            }
            else
            {
                float curDistance = Mathf.Abs(transform.position.x - player.transform.position.x);
                if (curDistance < xOffsetSpawnDistance)
                {
                    isFalling = true;
                    GetComponent<Rigidbody2D>().velocity = fallSpeed;
                }
            }
        }

        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Damage")
        {

            GameObject asteroid1 = Instantiate(asteroidPrefab, transform.position, transform.rotation);
            SmallAsteroidScript as1 = asteroid1.GetComponent<SmallAsteroidScript>();
            as1.fallSpeed = new Vector2(fallSpeed.x + 2f, fallSpeed.y);
            as1.isFalling = true;
            as1.GetComponent<Rigidbody2D>().velocity = as1.fallSpeed;

            GameObject asteroid2 = Instantiate(asteroidPrefab, transform.position, transform.rotation);
            SmallAsteroidScript as2 = asteroid2.GetComponent<SmallAsteroidScript>();
            as2.fallSpeed = new Vector2(fallSpeed.x + -2f, fallSpeed.y);
            as2.isFalling = true;
            as2.GetComponent<Rigidbody2D>().velocity = as2.fallSpeed;

            GameObject asteroid3 = Instantiate(asteroidPrefab, transform.position, transform.rotation);
            SmallAsteroidScript as3 = asteroid3.GetComponent<SmallAsteroidScript>();
            as3.fallSpeed = new Vector2(fallSpeed.x + 0f, fallSpeed.y);
            as3.isFalling = true;
            as3.GetComponent<Rigidbody2D>().velocity = as3.fallSpeed;

            anim.SetTrigger("Explode");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Destroy(col.gameObject);

        }

        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("Explode");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }

    public void respawn()
    {
        GameObject newAss = Instantiate(gameObject, start, transform.rotation);
        BigAsteroidScript ass = newAss.GetComponent<BigAsteroidScript>();
        ass.fallSpeed = fallSpeed;
        ass.despawnDistance = despawnDistance;
        ass.GetComponent<Collider2D>().enabled = true;
        ass.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        ass.GetComponent<SpriteRenderer>().enabled = true;
        ass.xOffsetSpawnDistance = xOffsetSpawnDistance;
        ass.player = player;
        ass.isFalling = false;
        ass.GetComponent<Animator>().Play("Normal");
        ass.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

}
