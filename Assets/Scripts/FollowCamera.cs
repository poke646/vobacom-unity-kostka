using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public static FollowCamera instance;
    public Transform target;
    public float followSpeed = 2f;
    private Vector3 offset;

    void Start ()
    {
        instance = this;
        offset = transform.position - target.transform.position;
    }

    public void SetOnTarget()
    {
        transform.position = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);
    }
	
	void LateUpdate () {
        float f = followSpeed * Time.deltaTime;

        Vector3 newPos = new Vector3(
            Mathf.Lerp(transform.position.x, target.position.x+offset.x, f),
            transform.position.y,
            Mathf.Lerp(transform.position.z, target.position.z+offset.z, f)
        );

        transform.position = Vector3.Lerp(transform.position, newPos, f);
    }
}
