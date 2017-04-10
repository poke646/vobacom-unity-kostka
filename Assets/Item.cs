﻿using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            GameObject go = GameObject.Find("Finish");
            if (go != null)
                go.GetComponent<Finish>().NumGoldLeft--;

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		if (gameObject.CompareTag("Gold"))
        {
            GameObject go = GameObject.Find("Finish");
            if (go != null)
                go.GetComponent<Finish>().NumGoldLeft++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
