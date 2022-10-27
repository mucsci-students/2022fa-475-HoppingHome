using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{

    public float speed = 3f;
    public int health = 3;
    public bool angered = false;
    public Transform leftBound;
    public Transform rightBound;

    private float lb;
    private float rb;
    private Transform start;
    private bool isRight = false;
    private GameObject player;
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;

    public GameObject bullet;
    public float bulletSpeed = 1f;
    public float bulletCooldown = 2.00f;
    private float timer = 0f;

    private Animator anim;    

    void Start()
    {
        start = transform;
        rb2D = GetComponent<Rigidbody2D>();
        lb = leftBound.position.x;
        rb = rightBound.position.x;
        sr = gameObject.GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xOffSet = -0.5f;
        if (!angered)
        {
            if ((isRight && transform.position.x >= rb) || 
                (!isRight && transform.position.x <= lb))
            {
                flip();
            }
            if (isRight)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(rb, transform.position.y), speed * Time.deltaTime);
            } else {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(lb, transform.position.y), speed * Time.deltaTime);
            }
        } else {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + xOffSet, player.transform.position.y + 0.75f), speed * Time.deltaTime);
            if ((isRight && transform.position.x > player.transform.position.x + xOffSet) || 
                (!isRight && transform.position.x < player.transform.position.x + xOffSet))
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ziggy")
        {
            angered = true;
            player = col.gameObject;
        }
    }

    // void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.gameObject.name == "Ziggy")
    //     {
    //         angered = false;
    //     }
    // }

    private void flip()
    {
        // Face diff direction
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        // Fix position on flip
        transform.position = isRight 
            ? new Vector3(transform.position.x + sr.bounds.size.x, transform.position.y, transform.position.y)
            : new Vector3(transform.position.x - sr.bounds.size.x, transform.position.y, transform.position.y);
        isRight = !isRight;
    }

    private void fire()
    {
        Vector3 offsetRight = new Vector3 (sr.bounds.size.x / 2, -1.5f, 1.0f);
        Vector3 offsetLeft = new Vector3 (-sr.bounds.size.x / 2, -1.5f, 1.0f);

        if (isRight)
        {
            GameObject temp = Instantiate(bullet, transform.position + offsetRight, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = (player.transform.position - temp.transform.position).normalized * bulletSpeed;
        } else {
            GameObject temp = Instantiate(bullet, transform.position + offsetLeft, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = (player.transform.position - temp.transform.position).normalized * bulletSpeed;
        }
        anim.SetTrigger("Shoot");
    }
}
