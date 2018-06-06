using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MedusaScript : MonoBehaviour {

    //health
    public Image healthBar;
    public float totalHealth = 15f;
    public float currentHealth = 15;
    public bool isDead = false;

    //vision/movement
    public BoxCollider2D m_MedusaMovement;
    public BoxCollider2D m_MedusaVision;
    public float MedusaSpeed = 3f;
    public bool m_MovingRight = false;
    public GameObject m_Target;

    //sound effect
    public AudioSource m_ASource;
    public AudioClip m_HissSFX;

    //projectiles
    public GameObject m_Arrows;
    const float ARROW_SPEED = 4f;
    List<GameObject> m_ArrowsList = new List<GameObject>();

    Animator m_Animator;
    SpriteRenderer m_Renderer;

    public enum MedusaAIState
    {
        Idle,
        Wander,
        Shooting
    }

    //create var to hold type
    public MedusaAIState m_MedusaState = MedusaAIState.Wander;

	// Use this for initialization
	void Start () {

        m_Animator = GetComponent<Animator>();
        m_Renderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        //update health
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        healthBar.fillAmount = currentHealth / totalHealth;

        //death
        if (currentHealth <= 0.01)
        {
            isDead = true;
            m_Animator.SetBool("isDead", true);
        }

        if (!isDead)
        {
            switch (m_MedusaState)
            {
                case MedusaAIState.Idle:
                    //MedusaIdle();
                    break;
                case MedusaAIState.Wander:
                    MedusaWandering();
                    break;
                case MedusaAIState.Shooting:
                    MedusaShooting();
                    break;
                default:
                    break;
            }
        }
	}

    void MedusaWandering()
    {
        m_Animator.SetBool("isWalking", true);
        Vector3 pos = transform.position;

        if (m_MovingRight)
        {
            m_Renderer.flipX = false;

            if (pos.x < m_MedusaMovement.transform.position.x + m_MedusaMovement.size.x / 2f)
            {
                transform.position = new Vector3(pos.x + MedusaSpeed * Time.deltaTime, pos.y, pos.z);
            }

            else
            {
                m_MovingRight = false;
            }

            pos = new Vector3(pos.x + MedusaSpeed * Time.deltaTime, pos.y, pos.z);

        }

        else
        {
            m_Renderer.flipX = true;

            if (pos.x > m_MedusaMovement.transform.position.x - m_MedusaMovement.size.x / 2f)
            {
                m_Renderer.flipX = true;
                transform.position = new Vector3(pos.x - MedusaSpeed * Time.deltaTime, pos.y, pos.z);
            }

            else
            {
                m_MovingRight = true;
            }
        }
    }

    void MedusaShooting()
    {
        m_Animator.SetBool("isShooting", true);

        //look at player
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && !isDead)
        {
            if (collision.GetType() == typeof(BoxCollider2D))
            {
                m_ASource.PlayOneShot(m_HissSFX);
                m_Target = collision.gameObject;
                m_MedusaState = MedusaAIState.Shooting;

                //determine angle to fire at
                Vector3 dir = collision.gameObject.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x);
                Quaternion lookRot = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);

                Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 1f, 0);

                GameObject Arrow = (GameObject)Instantiate(m_Arrows, spawnPos, lookRot);
                m_ArrowsList.Add(m_Arrows);
                SpriteRenderer renderer = Arrow.GetComponent<SpriteRenderer>();
                Rigidbody2D rigidBody = Arrow.GetComponent<Rigidbody2D>();

                if (m_Renderer.flipX)
                {
                    rigidBody.AddForce(dir * ARROW_SPEED, ForceMode2D.Impulse);

                }
                else
                {
                    rigidBody.AddForce(dir * ARROW_SPEED, ForceMode2D.Impulse);
                    renderer.flipX = true;
                }
            }

            else if (collision.GetType() == typeof(CircleCollider2D))
            {
                m_Animator.SetBool("isAttacking", true);
                collision.gameObject.GetComponent<PlayerHealth>().currentHealth -= 3;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            if (collider.GetType() == typeof(BoxCollider2D))
            {
                m_MedusaState = MedusaAIState.Wander;
                m_Animator.SetBool("isShooting", false);
                //m_Target = null;
            }
        }
    }
}
