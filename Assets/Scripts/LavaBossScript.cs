using UnityEngine;
using System.Collections;

public class LavaBossScript : MonoBehaviour {

    public GameObject target;
    public float ratio = 0.05f;

	// Use this for initialization
	void Start () {

        if (target == null)
        {
            //target player
            target = GameObject.FindGameObjectWithTag("Player");
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        
        //move towards player
        //add grounding, and flipping x
        //include a sight range
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, ratio);
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<PlayerHealth>().currentHealth -= 3;
        }
    }
}
