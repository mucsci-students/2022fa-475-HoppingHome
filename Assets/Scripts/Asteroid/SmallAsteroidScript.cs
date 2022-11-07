using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroidScript : MonoBehaviour
{

    public Vector2 fallSpeed = new Vector2(0, -5);
    public float xOffsetSpawnDistance = 10f;
    public float despawnDistance = 100f;
    public bool isFalling = false;
    public GameObject player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Ziggy");
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            Vector3 bulletPos = transform.position;
            float curDistance = Vector3.Distance(bulletPos, player.transform.position);
            if (curDistance > despawnDistance)
            {
                Destroy(gameObject);
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

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Damage")
        {
            anim.SetTrigger("Explode");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("Explode");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Destroy(gameObject);
        }
    }

}
