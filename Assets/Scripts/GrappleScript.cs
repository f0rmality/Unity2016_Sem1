using UnityEngine;
using System.Collections;

public class GrappleScript : MonoBehaviour {

    public LineRenderer grappleLine;

    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    Animator m_Animator;

    public GameObject grappleOrigin;
    public float maxDistance = 10f;
    public LayerMask mask;
    public float step = 0.2f;

	// Use this for initialization
	void Start () {

        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grappleLine.enabled = false;

        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            //find mouse position

            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            //fires raycast to mouse position based on max distance
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, maxDistance, mask);

            //if it hits something with a collider
            if (hit.collider != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);

                //enables line renderer, sets positions to player and hit point
                //set position one slightly higher to make it come from his hand
                grappleLine.enabled = true;
                grappleLine.SetPosition(0, grappleOrigin.transform.position);
                grappleLine.SetPosition(1, hit.point);


                m_Animator.SetBool("isGrappling", true);
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            grappleLine.SetPosition(0, grappleOrigin.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            joint.enabled = false;
            grappleLine.enabled = false;
            m_Animator.SetBool("isGrappling", false);
        }
	
	}
}
