using System;
using UnityEngine; 

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        private float maxSpeedCopy;
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character


        public int health = 3;
        private int startHealth;

        public Animator m_Anim;                // Reference to the player's animator.
        private Rigidbody2D m_Rigidbody2D;      // Reference to the player's Rigidbody.
        private Transform m_GroundCheck;        // A position marking where to check if the player is grounded.
        private Transform m_CeilingCheck;       // A position marking where to check for ceilings
        private Transform m_RightCheck;         // A position marking where to check for wall jumps
        private Transform m_LeftCheck;          // A position marking where to check for wall jumps

        private const float k_GroundedRadius = .2f;     // Radius of the overlap circle to determine if grounded
        private const float k_CeilingRadius = .01f;     // Radius of the overlap circle to determine if the player can stand up
        private const float m_CrouchSpeed = 0f;         // Movement speed when crouched
        private bool m_Grounded;                // Whether or not the player is grounded.
        private bool m_wall;
        public bool m_FacingRight = true;      // For determining which way the player is currently facing.
        private bool isJumping;                 // Whether or not the player is in the air with jumpHoldDuration.

        public float jumpHoldDuration = 0.25f;  // Max duration to hold space and gain velocity.
        private float jumpHoldCounter;          // Control for jumpHoldDuration
        //private float normalMass = 1.0f;
        //private float shieldDummyThickFatmAss = 1000.0f;

        public Transform m_spawn;               // For player spawns and checkpoints

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_RightCheck = transform.Find("OffSetRight");
            m_LeftCheck = transform.Find("OffSetLeft");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            // Set up player start
            m_Rigidbody2D.position = m_spawn.position;
            startHealth = health;
            maxSpeedCopy = m_MaxSpeed;
        }

        private void FixedUpdate()
        {
            m_Grounded = false;
            m_wall = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    break;
                }
            }

            // Right Wall Check
            if (!m_Grounded && m_Rigidbody2D.velocity.y <= 7)
            {
                // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
                colliders = Physics2D.OverlapCircleAll(m_RightCheck.position, k_GroundedRadius, m_WhatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag != "Damage")
                    {
                        Debug.Log(colliders[i]);
                        m_Grounded = true;
                        m_wall = true;
                        break;
                    }
                }
            }

            // Left wall check
            if (!m_Grounded && m_Rigidbody2D.velocity.y <= 7)
            {
                // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
                colliders = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_GroundedRadius, m_WhatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag != "Damage")
                    {
                        Debug.Log(colliders[i]);
                        m_Grounded = true;
                        m_wall = true;
                        break;
                    }
                }
            }
            
            // Set Ground for animation
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            m_Anim.SetBool("FacingRight", m_FacingRight);
        }

        public void Move(float move, bool shield, bool jump, bool jump_2)
        {
            // If crouching, check to see if the character can stand up
            // if (!shield && m_Anim.GetBool("Shield"))
            // {
            //     // If the character has a ceiling preventing them from standing up, keep them crouching
            //     if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            //     {
            //         shield = true;
            //     }
            // }

            m_Anim.SetFloat("yPos", m_Rigidbody2D.position.y);

            // Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Shield", shield);
        
            // if(shield)
            // {
            //     m_Rigidbody2D.mass = shieldDummyThickFatmAss;
            // }
            // if(!shield)
            // {
            //     m_Rigidbody2D.mass = normalMass;
            // }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                //if(shield){
                    //move = (shield ? move * m_CrouchSpeed: move);
                    //m_Anim.SetBool("Ground",false);
                //}
                
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                //If moving right and facing left:
                if (move > 0 && !m_FacingRight)
                {   
                    Flip();
                }
                // If moving left and facing right:
                else if (move < 0 && m_FacingRight)
                {
                    Flip();
                }

            }

            /**** Working static jump height (change jump force if not working)****/

            // if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            // {
            //     // Add a vertical force to the player.
            //     m_Grounded = false;
            //     m_Anim.SetBool("Ground", false);
            //     m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            // }

            /**** Dynamic jump height, can & will make better ****/
            //if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            if (m_Grounded && jump)
            {
                // While in air, change states
                m_Grounded = false;
                isJumping = true;

                // Set state for animator
                m_Anim.SetBool("Ground", false);

                // Initialize jump interval
                jumpHoldCounter = jumpHoldDuration;
                // Juuump!
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce + 250f));
            }

            if (m_wall && jump)
            {
              // While in air, change states
              m_Grounded = false;
              isJumping = true;

              // Set state for animator
              m_Anim.SetBool("Ground", false);

              // Initialize jump interval
              jumpHoldCounter = jumpHoldDuration;
              // Juuump!
              //m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce + 450f));
              m_Rigidbody2D.velocity = new Vector2(0f, 3f);
            }
            
            // If HOLDING jump && in the air:
            if (jump_2 && isJumping)
            {
                if (jumpHoldCounter > 0)
                {
                    // Reduce timer and jump
                    jumpHoldCounter -= Time.deltaTime;
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce *.95f));
                }
                // No more jumping for you.
                else { 
                    isJumping = false;
                    jumpHoldCounter = 0;
                }
            }
            // No more jumping for you x2.
            if (!jump_2) 
            { 
                isJumping = false; 
                jumpHoldCounter = 0;
            }
        }

        // Flip player depending on the way they are / should be facing.
        public void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void respawn()
        {
            m_Rigidbody2D.position = m_spawn.position;
            health = startHealth;
            GetComponent<RespawnManager>().respawn();
            m_MaxSpeed = maxSpeedCopy;
            GetComponent<Platformer2DUserControl>().respawn();
        }

        // Fires a bullet at a given speed, called from character inputs script
        public void fire(GameObject bullet, Vector2 bulletSpeed)
        {
            Vector3 offsetRight = new Vector3 (-0.3f, -0.3f, 0.0f);
            Vector3 offsetLeft = new Vector3 (0f, -0.3f, 0.0f);

            if (m_FacingRight)
            {
                GameObject temp = Instantiate(bullet, m_Rigidbody2D.transform.localPosition + offsetRight, Quaternion.identity);
                Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
                rb.velocity = bulletSpeed;
            } else {
                GameObject temp = Instantiate(bullet, m_Rigidbody2D.transform.localPosition + offsetLeft, Quaternion.identity);
                Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
                rb.velocity = bulletSpeed * -1f;
            }
            m_Anim.SetTrigger("Shoot");
        }
    }
}
