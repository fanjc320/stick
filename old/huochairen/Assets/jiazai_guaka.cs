using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiazai_guaka : MonoBehaviour {
    //重生点
    private Vector2 positions_;
    private Animator play;
    int death_id = Animator.StringToHash("Is_dead");
	// Use this for initialization
	void Start () {
        level_con.action += living_position;
        play = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //保存相应的重生位置
    void living_position(Vector2 positions)
    {
        positions_ = positions;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "danger")
        {
            play.SetBool(death_id, true);
            this.gameObject.GetComponent<player_control>().enabled = false;
            this.gameObject.GetComponent<jiazai_guaka>().enabled = false;
            StartCoroutine("rebirth", 2);
        }
    }

    //重生
    IEnumerator rebirth(float num)
    {        
        yield return new WaitForSeconds(num);
        GameObject game = GameObject.Instantiate(this.gameObject);
        GameObject.Destroy(this.gameObject);
        game.transform.position = positions_;
    }

}
