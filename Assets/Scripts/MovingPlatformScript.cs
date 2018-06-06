using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatformScript : MonoBehaviour {

    //vision/movement
    public BoxCollider2D m_PlatformMovement;
    public float PlatformSpeed = 1f;
    public bool m_MovingRight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Moving();

	}

    void Moving()
    {
        Vector3 pos = transform.position;

        if (m_MovingRight)
        {

            if (pos.x < m_PlatformMovement.transform.position.x + m_PlatformMovement.size.x / 2f)
            {
                transform.position = new Vector3(pos.x + PlatformSpeed * Time.deltaTime, pos.y, pos.z);
            }

            else
            {
                m_MovingRight = false;
            }

            pos = new Vector3(pos.x + PlatformSpeed * Time.deltaTime, pos.y, pos.z);

        }

        else
        {

            if (pos.x > m_PlatformMovement.transform.position.x - m_PlatformMovement.size.x / 2f)
            {
                transform.position = new Vector3(pos.x - PlatformSpeed * Time.deltaTime, pos.y, pos.z);
            }

            else
            {
                m_MovingRight = true;
            }
        }
    }
}
