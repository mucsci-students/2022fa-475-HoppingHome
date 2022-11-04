using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopScript : MonoBehaviour
{

    public float speed = 3f;
    public int health = 3;
    public bool angered = false;
    public float detectionRadius = 15f;
    public GameObject player;

    private Transform start;
    private bool isRight = true;
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;

    // Bullet stuff
    public GameObject bullet;
    public float bulletSpeed = 1f;
    public float bulletCooldown = 1.00f;
    private float timer = 0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        start = transform;
        rb2D = GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (player == null)
        {
            player = GameObject.Find("Ziggy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!angered)
        {
            float curDistance = Vector3.Distance(transform.position, player.transform.position);
            if (curDistance < detectionRadius)
            {
                angered = true;
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);

            if ((isRight && player.transform.position.x < transform.position.x) || 
                (!isRight && player.transform.position.x > transform.position.x))
            {
                flip();
            }

            //Shoot
            if (timer > bulletCooldown)
            {
                fire();
                timer = 0f;
            }

            timer += Time.deltaTime;
        }
    }

    private void flip()
    {
        // Face diff direction
        sr.flipX = isRight;
        isRight = !isRight;
    }

    private void fire()
    {
        Vector3 offsetRight = new Vector3(0, -0.2f, 1.0f);
        Vector3 offsetLeft = new Vector3(0, -0.2f, 1.0f);

        anim.SetTrigger("shoot");

        if (isRight)
        {
            GameObject temp = Instantiate(bullet, transform.position + offsetRight, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = (player.transform.position - temp.transform.position).normalized * bulletSpeed;
        }
        else
        {
            GameObject temp = Instantiate(bullet, transform.position + offsetLeft, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = (player.transform.position - temp.transform.position).normalized * bulletSpeed;
        }
        anim.SetTrigger("Shoot");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Damage")
        {
            --health;
            Destroy(col.gameObject);
            if (health <= 0)
            {
                anim.Play("Die");
                gameObject.SetActive(false);
            }
        }
    }
}
