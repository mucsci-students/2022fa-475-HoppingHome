using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
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
                Vector3 bulletPos = transform.position;
                float curDistance = Vector3.Distance(bulletPos, player.transform.position);
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
        AsteroidScript ass = newAss.GetComponent<AsteroidScript>();
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
