using System;
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

    //상수로 다음 수치? 에 도달하기 위한 이전 수치 선언
    public const int NEED_MIN = 60;
    public const int NEED_HOUR = 24;
    public const int NEED_DAY = 30;

    //요일을 담고있는 한글 배열
    public readonly string[] dayOfWeeks =
    {
        "월", "화", "수", "목", "금", "토", "일"
    };

    //시간 단위별로 바뀔 때마다 실행할 Action
    public Action<int> Add_Min = add_min => { };
    public Action<int> Add_Hour = add_hour => { };
    public Action<int> Add_Day = add_day => { };
    public Action<int> Add_Month = add_month => { };

    [Header("날짜, 시간을 담을 변수들")]
    //분, 시간, 일, 달, 요일을 담을 변수가 필요해
    public int now_min;
    public int now_hour;
    public int now_day;
    public int now_month;
    public string now_dayOfWeek;

    private int dayOfWeekIndex;

    [Header("날짜, 시간 텍스트")]
    public Text timeText;
    public Text dateText;

    [Header("디버그용 배속 변수")]
    public float multifilcation;

    //private WaitForSeconds ws_min;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //ws_min = new WaitForSeconds(ONE_MIN_SEC);

        UpdateText();

        StartCoroutine(TimeLogic());
    }

    //시간을 계속 더해주는 함수
    private IEnumerator TimeLogic()
    {
        while(true)
        {
            //게임시간 1분마다 코루틴을 실행
            yield return new WaitForSeconds(ONE_MIN_SEC * multifilcation);

            //게임시간 1분마다 1분을 더해준다
            now_min++;

            //1분이 변할때마다 함수 호출
            Add_Min(1);

            if (now_min >= NEED_MIN)
            {
                //한시간을 더해주고 현재 분을 초기화
                now_hour++;
                now_min = 0;

                //1시간이 변할때 마다 함수 호출
                Add_Hour(1);

                if (now_hour >= NEED_HOUR)
                {
                    //하루를 더해주고 현재 시간을 초기화
                    now_day++;
                    now_hour = 0;

                    now_dayOfWeek = dayOfWeeks[++dayOfWeekIndex % dayOfWeeks.Length];

                    //1일이 변할때 마다 함수 호출
                    Add_Day(1);

                    if (now_day > NEED_DAY)
                    {
                        //한달을 더해주고 현재 날짜를 초기화
                        now_month++;
                        //0일은 없으니까 1일로 초기화
                        now_day = 1;

                        //1달이 변할때 마다 함수 호출
                        Add_Month(1);
                    }
                }
            }

            //연산이 끝난 후 UI 업데이트
            UpdateText();
        }
    }

    public void UpdateText()
    {
        timeText.text = string.Concat(now_hour.ToString("00"), ":", now_min.ToString("00"));
        dateText.text = string.Concat(now_month.ToString("00"), "월 ", now_day.ToString("00"), "일 ", now_dayOfWeek, "요일");
    }
}
