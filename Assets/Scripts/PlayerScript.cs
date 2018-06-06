using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    public GameObject m_PlasmaBlast;

    SpriteRenderer m_Renderer;
    Animator m_Animator;
    Rigidbody2D m_RigidBody;

    //camera boundaries
    public static float MAX_X_BOUNDS;
    public static float MIN_X_BOUNDS;

    //ranged attack
    const float PLASMABLAST_SPEED = 7f;
    List<GameObject> m_PlasmaBlastList = new List<GameObject>();

    //audio
    public AudioSource m_ASource;
    public AudioClip m_PlasmaSFX;
    public AudioClip m_JumpingSFX;

    //levelswap
    public int level = 0;

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

        StartCoroutine(ShootPlasma());

	}


    void OnCollisionEnter2D(Collision2D collision)
    {

        BoxCollider2D m_PlatformCollider = collision.gameObject.GetComponent<BoxCollider2D>();

        //handle platform collisions

        if (collision.gameObject.layer == 9)
        {
            m_Animator.SetBool("isJumping", false);
            m_Animator.SetBool("isDoubleJumping", false);
            GetComponent<PlayerInputScript>().grounded = true;
        }

        //checks if player is on top
        if (transform.position.y > collision.gameObject.transform.position.y)
        {
            //allows you to jump again, grounds player
            m_Animator.SetBool("isJumping", false);
            m_Animator.SetBool("isDoubleJumping", false);
            GetComponent<PlayerInputScript>().grounded = true;
        }

        //change level on collision

        if (collision.gameObject.tag == "Level_End")
        {
            SceneManager.LoadScene(level);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 9)
        {
            m_Animator.SetBool("isJumping", false);
            m_Animator.SetBool("isDoubleJumping", false);
            GetComponent<PlayerInputScript>().grounded = true;
        }

        if (collision.gameObject.layer == 12)
        {
                //create a constant force downwards when touching the block
                m_Animator.SetBool("isJumping", false);
                m_Animator.SetBool("isDoubleJumping", false);
                m_RigidBody.AddForce(Vector2.down * 100);
        }

        if (collision.gameObject.layer == 15)
        {
            gameObject.GetComponent<PlayerHealth>().currentHealth -= 10;
        }
    }

    public void PlayJumpingSFX()
    {
        m_ASource.PlayOneShot(m_JumpingSFX);
    }

    IEnumerator ShootPlasma()
    {
        //find a way to delete the shots
        //add sounds
        //add collision

        if (Input.GetKeyDown(KeyCode.Space) && !m_Animator.GetBool("isJumping") && !m_Animator.GetBool("isWalking"))
        {
            m_ASource.PlayOneShot(m_PlasmaSFX);
            m_Animator.SetBool("isShooting", true);
            yield return new WaitForSeconds(1.0f);

            Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 1f, 0);


            GameObject plasmaBlast = (GameObject)Instantiate(m_PlasmaBlast, spawnPos, Quaternion.identity);
            m_PlasmaBlastList.Add(plasmaBlast);
            SpriteRenderer renderer = plasmaBlast.GetComponent<SpriteRenderer>();
            Rigidbody2D rigidBody = plasmaBlast.GetComponent<Rigidbody2D>();


            if (m_Renderer.flipX)
            {
                rigidBody.AddForce(-1 * (Vector2.left * PLASMABLAST_SPEED), ForceMode2D.Impulse);
                renderer.flipX = true;
            }
            else
            {
                rigidBody.AddForce(-1 * (Vector2.right * PLASMABLAST_SPEED), ForceMode2D.Impulse);
                renderer.flipX = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_Animator.SetBool("isShooting", false);
        }
    }
}
