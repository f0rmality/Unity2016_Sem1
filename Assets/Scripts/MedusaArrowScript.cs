using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MedusaArrowScript : MonoBehaviour {

    SpriteRenderer m_Renderer;
    public GameObject m_Target;
    const float ARROW_SPEED = 4f;

	// Use this for initialization
	void Start () {

        m_Renderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        ArrowAttack();
	
	}

    void ArrowAttack()
    {
        //to get direction
        Vector3 dir = m_Target.transform.position - transform.position;
        dir.Normalize();

        if (dir.x > 0)
        {
            m_Renderer.flipX = true;
        }

        else
        {
            m_Renderer.flipX = false;
        }

        //have arrow go after player
        transform.position += new Vector3(dir.x * (ARROW_SPEED * Time.deltaTime), 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<PlayerHealth>().currentHealth -= 3;
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
