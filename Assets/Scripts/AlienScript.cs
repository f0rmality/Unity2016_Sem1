using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlienScript : MonoBehaviour {

    Animator m_Animator;
    SpriteRenderer m_Renderer;
    Rigidbody2D m_RigidBody;

    //health
    public Image healthBar;
    public float totalHealth = 10f;
    public float currentHealth = 10;
    public bool isDead = false;

    //movement/AI
    public BoxCollider2D m_AlienVision;
    public GameObject m_Target;
    public float AlienSpeed = 1f;

    //create a type for AI
    public enum AlienAIState
    {
        Idle,
        Attacking
    }


    //create var to hold type
    public AlienAIState m_AlienState = AlienAIState.Idle;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_Renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //update health
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        healthBar.fillAmount = currentHealth / totalHealth;

        //death
        if (currentHealth <= 0.01)
        {
            isDead = true;
            m_Animator.SetBool("isDead", true);
            m_RigidBody.gravityScale = 1;
        }

        if (!isDead)
        {
            switch (m_AlienState)
            {
                case AlienAIState.Idle:
                    AlienIdle();
                    break;
                case AlienAIState.Attacking:
                    AlienAttacking();
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            if (collider.GetType() == typeof(BoxCollider2D))
            {
                m_AlienState = AlienAIState.Attacking;
                m_Target = collider.gameObject;
            }

            if (collider.GetType() == typeof(CircleCollider2D))
            {
                m_Animator.SetBool("isAttacking", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetType() == typeof(BoxCollider2D))
        {
            if (collider.gameObject.layer == 8)
            {
                m_AlienState = AlienAIState.Idle;
            }
        }
    }

    void AlienIdle()
    {
        m_Animator.SetBool("isAttacking", false);
    }

    void AlienAttacking()
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

        transform.position += new Vector3(dir.x * (AlienSpeed * Time.deltaTime), dir.y * (AlienSpeed * Time.deltaTime), 0);
    }
}
