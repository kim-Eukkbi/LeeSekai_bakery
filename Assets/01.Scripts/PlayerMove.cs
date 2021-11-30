using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    public string horizontalName;
    public string verticalName;

    //현재이동속도
    private float speed;
    //원래 이동속도
    public float originSpeed;
    //달릴 떄 이동속도
    public float runSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        speed = originSpeed;
    }

    void Update()
    {
        ChangeSpeed();
        Move();
        ChangeAnimation();
    }

    public void Move()
    {
        float h = Input.GetAxisRaw(horizontalName);
        float v = Input.GetAxisRaw(verticalName);

        transform.position += new Vector3(h, v, 0).normalized * speed * Time.deltaTime;
    }

    public void ChangeSpeed()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = originSpeed;
        }
    }

    public void ChangeAnimation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
            anim.SetBool("isWalk", true);
        }
        if(Input.GetKey(KeyCode.D))
        {
            sr.flipX = false;
            anim.SetBool("isWalk", true);
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isWalk", true);
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isWalk", false);
        }

        anim.SetFloat("moveSpeed", speed);
    }
}
