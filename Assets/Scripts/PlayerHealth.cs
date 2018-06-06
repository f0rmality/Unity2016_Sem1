using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    Animator m_Animator;

    //health
    public Image healthBar;
    public float totalHealth = 20f;
    public float currentHealth = 20;
    public bool isDead = false;

	// Use this for initialization
	void Start () {

        m_Animator = gameObject.GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

        //update health
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        healthBar.fillAmount = currentHealth / totalHealth;

        if (!isDead)
        {
            currentHealth += 0.005f;
        }

        //on death
        if (currentHealth <= 0.01)
        {
            isDead = true;
            //death animation, timer, load death screen, respawn key
            m_Animator.SetBool("isDead", true);
            //wait for 2 seconds
            SceneManager.LoadScene(6);
        }

        //if(gameObject.collider2D if collides with tag of deadly)
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //zombie
        if (collider.gameObject.layer == 17 && !collider.gameObject.GetComponent<ZombieScript>().isDead)
        {
            if (collider.GetType() == typeof(CircleCollider2D))
            {
                currentHealth -= 3;
            }
        }

        //alien
        if (collider.gameObject.layer == 18 && !collider.gameObject.GetComponent<AlienScript>().isDead)
        {
            if (collider.GetType() == typeof(CircleCollider2D))
            {
                currentHealth -= 5;
            }
        }

        //medusa
        if (collider.gameObject.layer == 19 && !collider.gameObject.GetComponent<MedusaScript>().isDead)
        {
            if (collider.GetType() == typeof(CircleCollider2D))
            {
                currentHealth -= 5;
            }
        }
    }
}
