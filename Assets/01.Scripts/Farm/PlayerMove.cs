using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [Header("TP관련")]
    public Vector2 farmTpPos;
    public Vector2 roadTpPos;
    public Vector2 bakeryTpPos;
    public Vector2 bakeryFrontTpPos;
    public bool isFarm = true;
    public CinemachineConfiner vcam;
    public Collider2D farmVcamConfiner;
    public Collider2D roadVcamConfiner;
    public Collider2D bakeryVcamConfiner;

    [Header("플레이어 팔길이")]
    public float range;

    //메인캠
    private Camera mainCam;
    //마우스 클릭 위치
    private Vector2 clickPos;

    //특정 행동(물주기, 씨뿌리기)하는 중인지
    private bool isPlayingAnim = false;
    public bool canMove = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        mainCam = Camera.main;

        speed = originSpeed;
    }

    void Update()
    {
        //clickPos를 사용하는 곳이 플레이어밖에 없어서 여기있는게 나을듯
        //나중에 InputManager 만들던가 해야지
        clickPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

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

    //clickPos를 받는 이유는 클릭 위치에 따라서 스프라이트를 뒤집어주기 위해서
    public void PlayAnimation(string animName)
    {
        //FarmManager에서 미리 isPlayAnim 검사하니까 여기서까지 처리할 필욘 없을듯

        sr.flipX = clickPos.x < transform.position.x;

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
    public bool IsInRange()
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
        //나중에 피직스2D 사각형 써가지고?
        //클릭지점에서 제일 가까운놈 가져오는것도 괜춘할듯
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //만약 도로포탈이라면
        if(col.CompareTag("RoadPortal"))
        {
            if(isFarm)
            {
                Teleport(roadTpPos, roadVcamConfiner);
            }
            else
            {
                Teleport(farmTpPos, farmVcamConfiner);
            }

            isFarm = !isFarm;
        }
        else if(col.CompareTag("BakeryPortal"))
        {
            Teleport(bakeryFrontTpPos, roadVcamConfiner);
        }
    }

    public void Teleport(Vector2 position, Collider2D vcamConfiner)
    {
        transform.position = position;
        vcam.m_BoundingShape2D = vcamConfiner;
    }
}
