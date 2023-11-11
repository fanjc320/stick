using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class player_control : MonoBehaviour {

    //动画播放器
    private Animator play_ani;
    private int forword_back_id=Animator.StringToHash("forword_back");
    private int up_down_id = Animator.StringToHash("up_down");
    private int left_right_id = Animator.StringToHash("left_right");//左右转
    private int left_right_run_id = Animator.StringToHash("left_right_run");//左右跑转
    private int sky_ground_id = Animator.StringToHash("sky_ground");
    private int Is_gun_id = Animator.StringToHash("Is_gun");
    private int Is_toforword_id = Animator.StringToHash("Is_toforword");
    //控制行走
    private BoxCollider2D collider;
    public static bool Is_go = true;
    //速度
    public float smooth = 0.5f;
    //目标位置
    private Vector2 target_x;
    private Vector2 target_y;
    private Vector2 ss;
    //跳跃、
    bool Is_true;
    bool Is_up=false;//禁止向上
    //获取物理处理
    private Rigidbody2D rigidbody;
    //计时器
    private float timer = 0;
    private float timer1 = 0;//第二次跳跃计时器
    private bool Is_two=false;//是否触发第二次跳跃
    //向上运动控制
    private int time_num = 0;
    //积分
    private int num=0;
    public Text text;
    
    // Use this for initialization
    void Start () {
        collider = this.GetComponent<BoxCollider2D>();
        target_x = this.transform.position;
        play_ani = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        food.action += jifen;
    }

    private void FixedUpdate()
    {
        left_right();
    }

    // Update is called once per frame
    void Update () {
        print(play_ani.GetBool(sky_ground_id));
        this.transform.up = Vector2.up;
        play_ani.SetFloat(forword_back_id,Input.GetAxis("Horizontal"));
        play_ani.SetFloat(up_down_id, Input.GetAxis("Vertical"));
        go_();
        tiao();
        
        //向上移动
        if (Is_true&& Is_go)
        {
            timer+=Time.deltaTime;
            if (timer > 0.6f)
            {               
                Is_true = false;
                rigidbody.simulated = true;
                play_ani.SetBool(Is_gun_id,false);
                return;
            }
            target_y = (Vector2)this.transform.position;
            target_y += Vector2.up *10f;
            this.transform.position = Vector2.Lerp((Vector2)this.transform.position, target_y, 0.1f * Time.deltaTime);
        }

        //向下
        if(Input.GetKeyDown(KeyCode.S))
        {
            down();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            play_ani.SetBool(sky_ground_id, false);
            play_ani.SetBool(Is_gun_id, false);
            collider.size = new Vector2(0.49f, 0.56f);
            
        }
    }


    void left_right()
    {
        if (Input.GetKey(KeyCode.A))
        {
            play_ani.SetBool(left_right_id, false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            play_ani.SetBool(left_right_id, true);
        }
    }

    //控制运动(左右)
    void go_()
    {
        string animString = play_ani.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        float x_ = Input.GetAxis("Horizontal");     
        if (animString == "run_back"&&x_>0.01)
            play_ani.SetBool(left_right_run_id, true);
        else if (animString == "run_forword" && x_ < -0.01)
            play_ani.SetBool(left_right_run_id, true);
        else if (animString == "run_back" && -0.01<x_&&x_<=0)
            play_ani.SetBool(left_right_run_id, false);
        else if (animString == "run_forword" && 0 <= x_ && x_ < 0.01)
            play_ani.SetBool(left_right_run_id, true);
        


        //移动（左右）
        if (x_ > 0&& Is_go)
        {
            target_x = this.transform.position;
            transform.position = target_x + new Vector2(0.8f * 0.04f, 0);
        }
        else if (x_ < 0&& Is_go)
        {
            target_x = this.transform.position;
            transform.position = target_x + new Vector2(-0.8f * 0.04f, 0);
        }
    }

    //跳跃（上下）
    void tiao()
    {
        if (Is_up)
            return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            time_num++;                                      
            if (time_num == 1)
            {
                rigidbody.simulated = false;
                play_ani.SetBool(sky_ground_id, true);
                Is_true = true;
                timer = 0;
            }
            else if (time_num == 2)
            {
                rigidbody.simulated = false;
                Is_true = true;
                timer1 = 0;
                timer = 0;
                play_ani.SetBool(Is_gun_id, true);
            }
        }
    }

    //射线检测

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
            play_ani.SetBool(sky_ground_id, false);
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "wall")
            time_num = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
            play_ani.SetBool(sky_ground_id, true);  
    }

   

    //向xia
    void down()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            play_ani.SetBool(Is_gun_id,true);
            play_ani.SetBool(sky_ground_id, false);
            collider.size = new Vector2(0.49f, 0.2f);
        }
    }

    //向左
    void left(GameObject obj,bool Is)
    {
        this.transform.position = ss;
        if (obj.tag == "wall")
        {
            Is_go = false;
            if (Is)
            {
                rigidbody.gravityScale = 0.1f;
            }
            else
                rigidbody.gravityScale = 0.8f;           
        }
    }

    //向右
    void right(GameObject obj, bool Is)
    {
        //this.transform.position = ss;
        if (obj.tag == "wall")
        {
            Is_go = false;
            if (Is)
            {
                rigidbody.gravityScale = 0.1f;
            }
            else
                rigidbody.gravityScale = 0.8f;
        }
    }

    //进行积分的操作
    void jifen()
    {
        print("积分");
        num++;
        text.text = num.ToString();
    }
}
