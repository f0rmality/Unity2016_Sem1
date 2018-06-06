using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlasmaShotScript : MonoBehaviour {

    public AudioSource m_ASource;
    public AudioClip m_PlasmaImpact;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 camPos = Camera.main.transform.position;

        //delete when offscreen
        if (transform.position.x - camPos.x > PlayerScript.MAX_X_BOUNDS 
            || transform.position.x - camPos.x < PlayerScript.MIN_X_BOUNDS)
        {
            Destroy(gameObject);
        }
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //fix this, make each if affect a different enemy
        //zombie
        if (collision.gameObject.layer == 17 && !collision.gameObject.GetComponent<ZombieScript>().isDead)
        {
            if (collision.GetType() == typeof(CircleCollider2D))
            {
                collision.gameObject.GetComponent<ZombieScript>().currentHealth -= 5;
                PlayPlasmaImpactSFX();
                Destroy(gameObject);
            }
        }

        //alien
        if (collision.gameObject.layer == 18 && !collision.gameObject.GetComponent<AlienScript>().isDead)
        {
            if (collision.GetType() == typeof(CircleCollider2D))
            {
                collision.gameObject.GetComponent<AlienScript>().currentHealth -= 5;
                PlayPlasmaImpactSFX();
                Destroy(gameObject);
            }
        }

        //medusa
        if (collision.gameObject.layer == 19 && !collision.gameObject.GetComponent<MedusaScript>().isDead)
        {
            if (collision.GetType() == typeof(CircleCollider2D))
            {
                collision.gameObject.GetComponent<MedusaScript>().currentHealth -= 5;
                PlayPlasmaImpactSFX();
                Destroy(gameObject);
            }
        }
    }

    void PlayPlasmaImpactSFX()
    {
        m_ASource.PlayOneShot(m_PlasmaImpact);
    }
}
