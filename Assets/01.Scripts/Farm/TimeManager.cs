using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    //이놈은 이제 머하는 놈이냐면
    //농장의 시간을 관리해주는 녀석이야
    //시간 단위를 대충 정리해보자면
    //1분은 2.5초
    //1시간은 150초
    //1일은 3600초

    //일단 상수로 선언부터 해놓자
    public const float ONE_MIN_SEC = 2.5f;
    public const float ONE_HOUR_SEC = 150f;
    public const float ONE_DAY_SEC = 3600f;

    //그냥 이전 단위가 얼마나 되었을 때를 체크하자
    //다음 단위로 가기 위해서 얼마나 필요한지를 선언
    private const float NEXT_MIN = 2.5f;
    private const int NEXT_HOUR = 60;
    private const int NEXT_DAY = 24;
    private const int NEXT_MONTH = 30;

    private readonly string[] dayOfWeekName =
    {
        "월", "화", "수", "목", "금", "토", "일"
    };

    //아직은 시간이 계속 초기화된다
    //그리고 이제 현재 시간을 담는 변수도 있어야겠지
    //Time.deltaTime을 계속 더해줄 sec 변수도 있어야겠지? 그냥 코루틴 돌려? 아니? 일단 해봐
    [Header("현재 농장의 시간")]
    public float now_sec = 0;
    public int now_min = 0;
    public int now_hour = 0;

    [Header("현재 농장의 날짜")]
    public int now_month = 1;
    public int now_day = 0;
    public string now_dayOfWeek = "월";
    private int now_dayOfWeek_index = 0;

    //아직은 시간관련밖에 안만들었음
    [Header("시간, 날짜 텍스트")]
    public Text timeText;
    public Text dateText;

    //디버깅 편하게 하기 위해서 배속 변수를 선언해놓자
    [Header("시간 배속 변수")]
    public float multiplication = 1f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        UpdateText();
    }

    private void Update()
    {
        //모든 시간의 기준이 되는 now_sec에 배속변수를 곱해준다
        now_sec += Time.deltaTime * multiplication;

        //톱니바퀴처럼 그냥 60분 지나면 1시간 더해주는 식으로 바꾸자
        if(now_sec >= NEXT_MIN)
        {
            //1분을 더해주고 초는 초기화
            now_min++;
            now_sec = 0;

            if(now_min >= NEXT_HOUR)
            {
                //1시간을 더해주고 분은 초기화
                now_hour++;
                now_min = 0;

                if(now_hour >= NEXT_DAY)
                {
                    //1일을 더해주고 시간은 초기화
                    now_day++;
                    now_hour = 0;

                    //다음요일로 바꿔준다
                    now_dayOfWeek = dayOfWeekName[now_dayOfWeek_index % dayOfWeekName.Length + 1];

                    if(now_day > NEXT_MONTH)
                    {
                        //1달을 더해주고 일은 초기화
                        now_month++;
                        now_day = 0;
                    }
                }
            }

            UpdateText();
        }
    }

    public void UpdateText()
    {
        timeText.text = string.Concat(now_hour.ToString("00"), ":", now_min.ToString("00"));
        dateText.text = string.Concat(now_month.ToString("00"), "월 ", now_day.ToString("00"), "일 ", now_dayOfWeek, "요일");
    }
}
