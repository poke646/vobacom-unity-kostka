using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject go = GameObject.Find("Finish");
            if (go != null)
                go.GetComponent<Finish>().NumGoldLeft--;

            Destroy(gameObject);
        }
    }

    void Start () {
		if (gameObject.CompareTag("Gold"))
        {
            GameObject go = GameObject.Find("Finish");
            if (go != null)
                go.GetComponent<Finish>().NumGoldLeft++;
        }
	}
}
