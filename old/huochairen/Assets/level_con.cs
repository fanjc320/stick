using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class level_con : MonoBehaviour {
    public static Action<Vector2> action;
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            action(collision.transform.position);
        }
    }
}
