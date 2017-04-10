using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    //public delegate void PlayerEventHandler(PlayerController player);
    //public event PlayerEventHandler playerEnter;
    //public event PlayerEventHandler playerLeave;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
    public void Kill()
    {
        rb.isKinematic = false;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
