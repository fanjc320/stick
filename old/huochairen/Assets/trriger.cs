using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trriger : MonoBehaviour {
    public Animator play_ani;
    
    private int sky_ground_id = Animator.StringToHash("sky_ground");

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("dfgfrt");
            play_ani.SetBool(sky_ground_id, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("dfgfrt");
            play_ani.SetBool(sky_ground_id, true);
        }
    }
}
