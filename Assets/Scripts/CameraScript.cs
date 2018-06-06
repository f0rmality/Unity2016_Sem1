using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    //for camera
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            Vector3 aheadPoint = target.position + new Vector3(4, 2, 0);
            Vector3 point = Camera.main.WorldToViewportPoint(aheadPoint);
            Vector3 delta = aheadPoint - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));

            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
