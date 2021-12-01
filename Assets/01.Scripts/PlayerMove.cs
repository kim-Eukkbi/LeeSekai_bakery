using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    //InputName��
    [Header("�Է�Ű")]
    public string horizontalName;
    public string verticalName;

    //�÷��̾� �ִϸ��̼� �̸���
    [Header("�ִϸ��̼� �̸���")]
    public string WaterName;
    public string PlantName;
    public string HarvestName;

    [Header("�ִϸ����� ������")]
    //���ϸ����� bool ����
    public string isWalkName;
    //���ϸ����� ���ǵ� ����
    public string speedName;

    [Header("�ӵ� ����")]
    //���� �̵��ӵ�
    public float originSpeed;
    //�޸� �� �̵��ӵ�
    public float runSpeed;
    //�����̵��ӵ�
    private float speed;

    [Header("�÷��̾� �ȱ���")]
    public float range;

    //Ư�� �ൿ(���ֱ�, ���Ѹ���)�ϴ� ������
    private bool isPlayingAnim = false;

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
        if (isPlayingAnim) return;

        float h = Input.GetAxisRaw(horizontalName);
        float v = Input.GetAxisRaw(verticalName);

        transform.position += new Vector3(h, v, 0).normalized * speed * Time.deltaTime;
    }

    public void ChangeSpeed()
    {
        if (isPlayingAnim) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
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
        if (isPlayingAnim) return;

        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
            anim.SetBool(isWalkName, true);
        }
        if(Input.GetKey(KeyCode.D))
        {
            sr.flipX = false;
            anim.SetBool(isWalkName, true);
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool(isWalkName, true);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool(isWalkName, false);
        }

        anim.SetFloat(speedName, speed);
    }

    public void PlayAnimation(string animName)
    {
        //FarmManager���� �̸� isPlayAnim �˻��ϴϱ� ���⼭���� ó���� �ʿ� ������

        anim.SetBool(isWalkName, false);
        anim.SetTrigger(animName);

        isPlayingAnim = true;
    }

    public void AnimEnd()
    {
        isPlayingAnim = false;
    }

    //�ൿ �� �� �ִ��� ����
    public bool CanAction()
    {
        return !isPlayingAnim;
    }

    //�����Ÿ� �ȿ� �ִ���
    public bool IsInRange(Vector2 clickPos)
    {
        //�Ÿ�üũ �� �������
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        if(Vector2.Distance(pos, clickPos) > range)
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
