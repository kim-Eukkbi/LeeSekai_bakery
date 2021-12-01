using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    //�̳��� ���� ���ϴ� ���̳ĸ�
    //������ �ð��� �������ִ� �༮�̾�
    //�ð� ������ ���� �����غ��ڸ�
    //1���� 2.5��
    //1�ð��� 150��
    //1���� 3600��

    //�ϴ� ����� ������� �س���
    public const float ONE_MIN_SEC = 2.5f;
    public const float ONE_HOUR_SEC = 150f;
    public const float ONE_DAY_SEC = 3600f;

    //����� ���� ��ġ? �� �����ϱ� ���� ���� ��ġ ����
    public const int NEED_MIN = 60;
    public const int NEED_HOUR = 24;
    public const int NEED_DAY = 30;

    //�ð� �������� �ٲ� ������ ������ Action
    public Action<int> Min_Change;
    public Action<int> Hour_Change;
    public Action<int> Day_Change;
    public Action<int> Month_Change;

    [Header("��¥, �ð��� ���� ������")]
    //��, �ð�, ��, ��, ������ ���� ������ �ʿ���
    public int now_min;
    public int now_hour;
    public int now_day;
    public int now_month;
    public string now_weekOfDay;

    private WaitForSeconds ws_min;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        ws_min = new WaitForSeconds(ONE_MIN_SEC);

        UpdateText();
    }

    private IEnumerator timeLogic()
    {
        //���ӽð� 1�и��� �ڷ�ƾ�� ����
        yield return ws_min;

        //���ӽð� 1�и��� 1���� �����ش�
        now_min++;

        if(now_min >= NEED_MIN)
        {
            //�ѽð��� �����ְ� ���� ���� �ʱ�ȭ
            now_hour++;
            now_min = 0;

            if(now_hour >= NEED_HOUR)
            {
                //�Ϸ縦 �����ְ� ���� �ð��� �ʱ�ȭ
                now_day++;
                now_hour = 0;

                if(now_day > NEED_DAY)
                {
                    //�Ѵ��� �����ְ� ���� ��¥�� �ʱ�ȭ
                    now_month++;
                    now_day = 0;
                }
            }
        }
    }

    public void UpdateText()
    {
        //timeText.text = string.Concat(now_hour.ToString("00"), ":", now_min.ToString("00"));
        //dateText.text = string.Concat(now_month.ToString("00"), "�� ", now_day.ToString("00"), "�� ", now_dayOfWeek, "����");
    }
}
