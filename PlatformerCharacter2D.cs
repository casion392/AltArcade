using System;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;



#pragma warning disable 649
namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] private float m_MaxSpeed = 3f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        public bool isPlayer1, isPlayer2;

        public Text timeText, player1Text, player2Text;
        public GameObject[] playuhs;
        private GameObject cube;
        private int[] xvals = {5,37,60,-22,-22};
        private int[] yvals = { -4, 22,-19,-17,13 };
        private float timeRemaining = 180;
        public bool timerIsRunning = false;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f;
        private bool tooClose = false;
        private bool startClose = false;
       
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

        public static int player1Score = 0, player2Score = 0;

        private Animator m_Anim;            // Reference to the player's animator component.

        private Rigidbody2D m_Rigidbody2D;

        public Rigidbody2D player1, player2;


        public bool it = false;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
 


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            timeText = GameObject.Find("Countdown").GetComponent<Text>();
            player1Text = GameObject.Find("Player1Score").GetComponent<Text>();
            player2Text = GameObject.Find("Player2Score").GetComponent<Text>();
            cube = GameObject.FindGameObjectWithTag("GameController");
            timerIsRunning = true;
            playuhs = GameObject.FindGameObjectsWithTag("Player");
            System.Random rnd = new System.Random();
            int posChange = rnd.Next(0, 5);
            cube.gameObject.transform.position = new Vector3(xvals[posChange], yvals[posChange], 0);

            
      
        }


        private void FixedUpdate()
        {
            player1Text.text = player1Score.ToString();
            player2Text.text = player2Score.ToString();
            m_Grounded = false;
          
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);


        // if(startClose == true){
        //     if (Vector3.Distance(player1.transform.position, player2.transform.position) < 0.75f){
        //         tooClose = true;
               
        //     }
        //     if (Vector3.Distance(player1.transform.position, player2.transform.position) > 0.75f)
        //         {
        //             tooClose = false;
        //             startClose = false;
        //         }
        //   }

            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    timerIsRunning = false;
                   

                }
            }
        

        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.name == "Changer"){
                Debug.Log("cube");
                if(isPlayer1 && !it){
                    it = true;
                    playuhs[1].GetComponent<PlatformerCharacter2D>().it = false;
                    System.Random rnd = new System.Random();
                    int posChange = rnd.Next(0, 5);
                    cube.gameObject.transform.position = new Vector3(xvals[posChange], yvals[posChange], 0);
                }
                if (isPlayer2 && !it)
                {
                    it = true;
                    playuhs[0].GetComponent<PlatformerCharacter2D>().it = false;
                    System.Random rnd = new System.Random();
                    int posChange = rnd.Next(0, 5);
                    cube.gameObject.transform.position = new Vector3(xvals[posChange], yvals[posChange], 0);
                }
            }

            
       // if(!tooClose){


            if(isPlayer1 && it && collision.gameObject.name == "Player2")
            {
                player1Score++;
               
                //Debug.Log("there");
                it = false;
                //collision.gameObject.GetComponent<PlatformerCharacter2D>().it = true;
                // startClose = true;
                // tooClose = true;
                
               
            }

            if (isPlayer2 && it && collision.gameObject.name == "Player1")
            {
                //Debug.Log("here");
                it = false;
                player2Score++;
                
                //collision.gameObject.GetComponent<PlatformerCharacter2D>().it = true;
                // startClose = true;
                // tooClose = true;
                  
                
            }
         // }
        }
        void DisplayTime(float timeToDisplay)
        {
            

            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        
            public void Move(float move, bool crouch, bool jump)
        {
            
            
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move / m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
