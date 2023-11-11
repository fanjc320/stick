using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ray_con : MonoBehaviour {
    public static Action<Collider2D> entry;
    private int Is_wallright_id = Animator.StringToHash("Is_towall_right");
    private int Is_wallleft_id = Animator.StringToHash("Is_towall_left");
    private int sky_ground_id = Animator.StringToHash("sky_ground");
    private Animator play_ani;
    private Rigidbody2D rigidbody;
    // Use this for initialization
    void Start () {
        play_ani = this.transform.parent.gameObject.GetComponent<Animator>();
        rigidbody = this.transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        ray_right();
        ray_left();
    }

    void ray_right()
    {
        RaycastHit2D hit;
        this.GetComponent<Collider2D>().enabled = false;
        hit = Physics2D.Linecast(this.transform.position, (Vector2)this.transform.position + new Vector2(0.15f, 0));
        this.GetComponent<Collider2D>().enabled = true;
        if (hit.transform != null && hit.collider.tag == "wall")
        {
            print("dfhgrtf" + play_ani.GetBool(Is_wallright_id));
            if(play_ani.GetBool(Is_wallright_id) ==false)
            play_ani.SetBool(Is_wallright_id, true);
        }
        else
        {
            player_control.Is_go = true;
            play_ani.SetBool(Is_wallright_id, false);
        }
    }
        void ray_left()
        {
            RaycastHit2D hit;
            this.GetComponent<Collider2D>().enabled = false;
            hit = Physics2D.Linecast(this.transform.position, (Vector2)this.transform.position - new Vector2(0.15f, 0));
            this.GetComponent<Collider2D>().enabled = true;
            if (hit.transform != null && hit.collider.tag == "wall")
            {
                print("dfhgrtf" + play_ani.GetBool(Is_wallright_id));
                if (play_ani.GetBool(Is_wallright_id) == false)
                    play_ani.SetBool(Is_wallright_id, true);
            }
            else
            {
                player_control.Is_go = true;
                play_ani.SetBool(Is_wallright_id, false);
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            play_ani.SetBool(sky_ground_id,true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            play_ani.SetBool(sky_ground_id, false);
        }
    }
}
