using UnityEngine;
using System.Collections;

public class FireElementalScript : MonoBehaviour {


    //create a type for AI
    public enum ElementalAIState
    {
        Idle,
        Patrol,
        Seeking,
        Attacking
    }

    //box collider
    public BoxCollider2D m_ElementalMovement;
    public BoxCollider2D m_ElementalVision;

    //target the player
    public GameObject m_Target;

    //speed
    public float ElementalSpeed = 2f;
    public float ElementalSeekSpeed = 5f;

    Animator m_Animator;
    SpriteRenderer m_Renderer;

    public bool m_MovingRight = false;

    //create var to hold type
    public ElementalAIState m_ElementalState = ElementalAIState.Patrol;

    // Use this for initialization
    void Start()
    {

        m_Animator = GetComponent<Animator>();
        m_Renderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        //switch behaviour based on AI state
        switch (m_ElementalState)
        {
            case ElementalAIState.Patrol:
                ElementalPatrol();
                break;
            case ElementalAIState.Seeking:
                ElementalSeek();
                break;
            case ElementalAIState.Attacking:
                ElementalAttack();
                break;
            default:
                break;
        }
	
	}

    //while(player not in range) patrol
    //if(player in range, target player) using MoveToward player method

    //health damage

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            m_ElementalState = ElementalAIState.Seeking;
            //GetComponent<Animator>().SetBool("isSeeking", true);
            m_Target = collider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            m_ElementalState = ElementalAIState.Patrol;
            //GetComponent<Animator>().SetBool("isSeeking", false);
            m_Target = null;
        }
    }

    void ElementalPatrol()
    {
        Vector3 pos = transform.position;
        m_Animator.SetBool("isMoving", true);

        //set it to pause at the ends

        if (m_MovingRight)
        {
            if (pos.x < m_ElementalMovement.transform.position.x + m_ElementalMovement.size.x / 2f)
            {
                transform.position = new Vector3(pos.x + ElementalSpeed * Time.deltaTime, pos.y, pos.z);
                m_Renderer.flipX = false;
            }

            else
            {
                m_MovingRight = false;
            }

            pos = new Vector3(pos.x + ElementalSpeed * Time.deltaTime, pos.y, pos.z);

        }

        else
        {
            if (pos.x > m_ElementalMovement.transform.position.x - m_ElementalMovement.size.x / 2f)
            {
                transform.position = new Vector3(pos.x - ElementalSpeed * Time.deltaTime, pos.y, pos.z);
                m_Renderer.flipX = true;
            }

            else
            {
                m_MovingRight = true;
            }
        }
    }

    void ElementalSeek()
    {
        //to get direction
        Vector3 dir = m_Target.transform.position - transform.position;
        dir.Normalize();

        if (dir.x > 0)
        {
            m_Renderer.flipX = false;
        }

        else
        {
            m_Renderer.flipX = true;
        }

        //have elemental run after player
        transform.position += new Vector3(dir.x * (ElementalSeekSpeed * Time.deltaTime), 0, 0);
    }

    void ElementalAttack()
    {
        //if colliding with player
        //switch to attack animation
        //damage player health
    }
}
