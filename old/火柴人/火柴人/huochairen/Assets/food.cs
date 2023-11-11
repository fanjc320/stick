using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class food : MonoBehaviour {

    public static Action action;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (action != null)
            {
                action();
            }
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
