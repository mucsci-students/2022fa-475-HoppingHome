using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump_down;
        private bool m_Jump_button;
        public AudioSource source;
        public AudioClip heal;

        //Bullet code
        public float bulletCooldown = 0.25f;
        private float timer = 0f;
        public GameObject bullet;
        public Vector2 bulletSpeed = new Vector2(10f, 0f);

        public float invincibleLength = 0.25f;
        private float invincibleTimer = 0f;
        public bool invincible = false;
        public Image shieldImg;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            source = GetComponent<AudioSource>();
            heal = GetComponent<AudioClip>();

            if (shieldImg == null)
            {
                shieldImg = GameObject.Find("ShieldImg").GetComponent<Image>();
            }
        }


        private void Update()
        {
            if (!m_Jump_down)
            {
                // m_Jump_down when jump is pressed
                m_Jump_down = Input.GetButtonDown("Jump");
                // m_Jump_button DURATION jump is pressed
                m_Jump_button = Input.GetButton("Jump");
            }

            //Shoot
            if (Input.GetButtonDown("Fire1") && timer > bulletCooldown)
            {
                m_Character.fire(bullet, bulletSpeed);
                timer = 0f;
            }

            timer += Time.deltaTime;

            if (invincible)
            {
                invincibleTimer += Time.deltaTime;
                if (invincibleTimer > invincibleLength)
                {
                    invincible = false;
                    invincibleTimer = 0f;
                }
            }

            if (invincible)
            {
                shieldImg.fillAmount = 1 - (invincibleTimer / invincibleLength);
            } else
            {
                shieldImg.fillAmount = 0;
            }
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool shield = Input.GetKey(KeyCode.S);
            float h = Input.GetAxis("Horizontal");
                          
            // Pass all parameters to the character control script.
            m_Character.Move(h, shield, m_Jump_down, m_Jump_button);
            m_Jump_down = false;
            m_Jump_button = false;
            shield = false;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Powerup")
            {
                heal = source.clip;
                source.PlayOneShot(heal);
            }
            else if (collision.gameObject.tag == "EnemyDamage")
            {
                if (!invincible)
                {
                    m_Character.m_Anim.SetTrigger("TakeDamage");
                    m_Character.health--;
                    invincible = true;
                }
            }
            else if (collision.gameObject.tag == "Kill")
            {
                
                m_Character.health = 0;
            }

            if (m_Character.health <= 0) 
            {
                m_Character.respawn();
            }
        }
    }
}
