using UnityEngine;
using System.Collections;

public class MainGameScript : MonoBehaviour {

    public GameObject m_Player;
    public GameObject[] m_BackgroundObjectArray;
    public GameObject[] m_MidGroundObjectsArray;
    public GameObject[] m_ForeGroundObjectsArray;

    const float MID_GROUND_SPEED = 0.01f;
    const float FORE_GROUND_SPEED = 0.02f;

    int m_BackgroundIndex = 1;
    float m_BackgroundSize;
    int m_MidGroundRightMost = 3;
    int m_MidGroundLeftMost = 0; 



	// Use this for initialization
	void Start () {

        m_BackgroundSize = m_BackgroundObjectArray[0].GetComponent<SpriteRenderer>().bounds.size.x;
	
	}
	
	// Update is called once per frame
	void Update () {

        //ParallaxBackground();
        MoveMidGround();
        MoveForeGround();
	
	}

    void ParallaxBackground()
    {
        GameObject rightBackground = m_BackgroundObjectArray[m_BackgroundIndex];
        GameObject leftBackground = m_BackgroundObjectArray[(m_BackgroundIndex >= m_BackgroundObjectArray.Length - 1) ? 0 : m_BackgroundIndex + 1];

        if (m_Player.transform.position.x >= rightBackground.transform.position.x)
        {
            if (m_BackgroundIndex < m_BackgroundObjectArray.Length - 1)
                m_BackgroundIndex++;

            else
                m_BackgroundIndex = 0;

            m_BackgroundObjectArray[m_BackgroundIndex].transform.position = new Vector3(rightBackground.transform.position.x + m_BackgroundSize, rightBackground.transform.position.y, 0f);

            Debug.Log("POSITIONING TO THE RIGHT");

        }
        else if (m_Player.transform.position.x < leftBackground.transform.position.x && m_Player.transform.position.x > 0)
        {
            m_BackgroundObjectArray[m_BackgroundIndex].transform.position = new Vector3(leftBackground.transform.position.x - m_BackgroundSize, rightBackground.transform.position.y, 0f);
            Debug.Log("POSITIONING TO THE LEFT");

        }
    }


    void MoveMidGround()
    {
        if (m_Player.transform.position.x - m_Player.GetComponent<SpriteRenderer>().bounds.extents.x > PlayerInputScript.MIN_X_BOUNDS)
        {
            for (int i = 0; i < m_MidGroundObjectsArray.Length; i++)
            {
                Vector3 midPos = m_MidGroundObjectsArray[i].transform.position;

                //This line makes our Midground elements move based on how fast the player is moving

                m_MidGroundObjectsArray[i].transform.position = new Vector3(midPos.x - (Input.GetAxis("Horizontal") * (1 + Input.GetAxis("Acceleration"))) * MID_GROUND_SPEED, midPos.y, midPos.z);

                GameObject midGround = m_MidGroundObjectsArray[i];
                float midGroundExtent = midGround.GetComponent<SpriteRenderer>().bounds.extents.x;
            }
        }

    }

    void MoveForeGround()
    {
        if (m_Player.transform.position.x - m_Player.GetComponent<SpriteRenderer>().bounds.extents.x > (PlayerInputScript.MIN_X_BOUNDS + (PlayerInputScript.MAX_X_BOUNDS / 2)))
        {
            for (int i = 0; i < m_ForeGroundObjectsArray.Length; i++)
            {
                Vector3 forePos = m_ForeGroundObjectsArray[i].transform.position;

                //This line makes our Midground elements move based on how fast the player is moving

                m_ForeGroundObjectsArray[i].transform.position = new Vector3(forePos.x - (Input.GetAxis("Horizontal") * (1 + Input.GetAxis("Acceleration"))) * FORE_GROUND_SPEED, forePos.y, forePos.z);

                GameObject foreGround = m_ForeGroundObjectsArray[i];
                float foreGroundExtent = foreGround.GetComponent<SpriteRenderer>().bounds.extents.x;
            }
        }

    }
}
