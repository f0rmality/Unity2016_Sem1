using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZombieScript : MonoBehaviour {


    Animator m_Animator;
    SpriteRenderer m_Renderer;

    //health
    public Image healthBar;
    public float totalHealth = 10f;
    public float currentHealth = 10;
    public bool isDead = false;

    //movement/AI
    public BoxCollider2D m_ZombieVision;
    public GameObject m_Target;
    public float ZombieSpeed = 1f;

    //create a type for AI
    public enum ZombieAIState
    {
        Idle,
        Seeking
    }


    //create var to hold type
    public ZombieAIState m_ZombieState = ZombieAIState.Idle;

	// Use this for initialization
	void Start () {

        m_Animator = gameObject.GetComponent<Animator>();
        m_Renderer = gameObject.GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

        //update health
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        healthBar.fillAmount = currentHealth / totalHealth;

        //on death
        if (currentHealth <= 0.01)
        {
            //destroy thing
            isDead = true;
            m_Animator.SetBool("isDead", true);
        }

        if (!isDead)
        {
            switch (m_ZombieState)
            {
                case ZombieAIState.Idle:
                    ZombieIdle();
                    break;
                case ZombieAIState.Seeking:
                    ZombieSeeking();
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
                m_ZombieState = ZombieAIState.Seeking;
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
                m_ZombieState = ZombieAIState.Idle;
            }
        }
    }

    void ZombieIdle()
    {
        m_Animator.SetBool("isWalking", false);
        m_Target = null;
    }

    void ZombieSeeking()
    {
        m_Animator.SetBool("isWalking", true);

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

        transform.position += new Vector3(dir.x * (ZombieSpeed * Time.deltaTime), 0, 0);
    }
}
