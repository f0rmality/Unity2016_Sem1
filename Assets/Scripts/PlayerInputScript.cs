using UnityEngine;
using System.Collections;

public class PlayerInputScript : MonoBehaviour {

    public static float MIN_X_BOUNDS;
    public static float MAX_X_BOUNDS;

    SpriteRenderer m_Renderer;
    Animator m_Animator;
    Rigidbody2D m_RigidBody;

    public float PLAYER_NORMAL_SPEED = 4f;
    const float PLAYER_EXTRA_SPEED = 5f;
    public float MIN_JUMP_FORCE = 15f;

    public bool doubleJumping = false;
    public bool grounded = true;

	// Use this for initialization
	void Start () {

        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();

        MIN_X_BOUNDS = -(Camera.main.aspect * Camera.main.orthographicSize);
        MAX_X_BOUNDS = Camera.main.aspect * Camera.main.orthographicSize;
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = gameObject.transform.position;
        Vector3 scale = gameObject.transform.localScale; 

        //walking controls
        if (Input.GetAxis("Horizontal") > 0)
        {
            m_Renderer.flipX = true;
            m_Animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            m_Renderer.flipX = false;
            m_Animator.SetBool("isWalking", true);
        }
        else
        {
            m_Animator.SetBool("isWalking", false);   
        }

        //running controls

        if (Input.GetKey(KeyCode.F) && grounded)
        {
            m_Animator.SetBool("isRunning", true);
        }
        else
        {
            m_Animator.SetBool("isRunning", false);
        }

        //ducking controls
        
        if (Input.GetKey(KeyCode.C))
        {
            m_Animator.SetBool("isDucking", true);
        }
        else
        {
            m_Animator.SetBool("isDucking", false);
        }

        //jumping controls

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                grounded = false;
                m_Animator.SetBool("isJumping", true);
                doubleJumping = true;
                m_RigidBody.AddForce(new Vector2(0, MIN_JUMP_FORCE), ForceMode2D.Impulse);
                m_RigidBody.gravityScale = 0.7f;

                GetComponent<PlayerScript>().PlayJumpingSFX();
            }

            else
            {
                if (doubleJumping)
                {
                    m_Animator.SetBool("isDoubleJumping", true);
                    //flipx ?
                    doubleJumping = false;
                    m_RigidBody.AddForce(new Vector2(0, MIN_JUMP_FORCE), ForceMode2D.Impulse);
                    m_RigidBody.gravityScale = 0.7f;
                }
            } 
        }
	}

    void FixedUpdate()
    {
        if (m_Animator.GetBool("isDucking"))
        {
            m_RigidBody.velocity = new Vector3(0, m_RigidBody.velocity.y, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (transform.position.x - m_Renderer.bounds.extents.x < PlayerInputScript.MIN_X_BOUNDS)
            {
                m_RigidBody.velocity = new Vector3(0, m_RigidBody.velocity.y, 0);
            }

            else
            {
                m_RigidBody.velocity = new Vector3(Input.GetAxis("Horizontal") * PLAYER_NORMAL_SPEED - PLAYER_EXTRA_SPEED * Input.GetAxis("Acceleration"),
                    m_RigidBody.velocity.y, 0);
            }
        }


        else if (Input.GetAxis("Horizontal") > 0)
        {
            m_RigidBody.velocity = new Vector3(Input.GetAxis("Horizontal") * PLAYER_NORMAL_SPEED + PLAYER_EXTRA_SPEED * Input.GetAxis("Acceleration"),
                m_RigidBody.velocity.y, 0);
        }

        else
        {
            m_RigidBody.velocity = new Vector3(0f, m_RigidBody.velocity.y, 0f);
        }
    }
}
