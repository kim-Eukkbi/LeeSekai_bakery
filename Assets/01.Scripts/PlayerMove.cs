using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    //InputName들
    [Header("입력키")]
    public string horizontalName;
    public string verticalName;

    //플레이어 애니메이션 이름들
    [Header("애니메이션 이름들")]
    public string WaterName;
    public string PlantName;
    public string HarvestName;

    [Header("애니메이터 변수들")]
    //에니메이터 bool 변수
    public string isWalkName;
    //에니메이터 스피드 변수
    public string speedName;

    [Header("속도 관련")]
    //원래 이동속도
    public float originSpeed;
    //달릴 떄 이동속도
    public float runSpeed;
    //현재이동속도
    private float speed;

    [Header("플레이어 팔길이")]
    public float range;

    //특정 행동(물주기, 씨뿌리기)하는 중인지
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
        //FarmManager에서 미리 isPlayAnim 검사하니까 여기서까지 처리할 필욘 없을듯

        anim.SetBool(isWalkName, false);
        anim.SetTrigger(animName);

        isPlayingAnim = true;
    }

    public void AnimEnd()
    {
        isPlayingAnim = false;
    }

    //행동 할 수 있는지 리턴
    public bool CanAction()
    {
        return !isPlayingAnim;
    }

    //사정거리 안에 있는지
    public bool IsInRange(Vector2 clickPos)
    {
        //거리체크 다 만들었다
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
