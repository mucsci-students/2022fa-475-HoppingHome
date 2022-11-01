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
        if (isFalling)
        {
            Vector3 bulletPos = transform.position;
            float curDistance = Vector3.Distance(bulletPos, player.transform.position);
            if (curDistance > despawnDistance)
            {
                Destroy(gameObject);
            }
        } else {
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
        if (col.gameObject.tag == "Damage")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }

    
}
