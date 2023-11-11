using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_C : MonoBehaviour {
    // Use this for initialization
    void Start () {
        this.GetComponent<player_control>().enabled = true;
        this.GetComponent<jiazai_guaka>().enabled = true;
        this.GetComponent<BoxCollider2D>().size = new Vector2(0.49f, 0.56f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
