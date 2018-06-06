using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    SpriteRenderer m_Renderer;
    Animator m_Animator;
    Rigidbody2D m_RigidBody;

    public GameObject[] m_EnemyArray = new GameObject[5];
    public GameObject m_Player;

	// Use this for initialization
	void Start () {

        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

        //AI for enemies
        for (int i = 0; i < m_EnemyArray.Length; i++)
        {
            GameObject enemy = m_EnemyArray[i];

            //enemies to face player no matter what
            //first get direction of player in ref to enemies
            //then use Quaternion to rotate enemies to the direction of the players

            Vector3 dir = enemy.transform.position - m_Player.transform.position;
            Quaternion lookRot = Quaternion.identity;

            //if to the left or to the right

            if (m_Player.transform.position.x < enemy.transform.position.x)
            {
                lookRot = Quaternion.LookRotation(dir, Vector3.up);
            }

            else
            {
                lookRot = Quaternion.LookRotation(dir, Vector3.down);
            }

            //Quaternion lookRot = Quaternion.LookRotation(dir);
            lookRot.x = 0;
            lookRot.y = 0;

            enemy.transform.rotation = lookRot;

        }
	}
}
