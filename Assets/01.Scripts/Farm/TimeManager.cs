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

    //������ ����ִ� �ѱ� �迭
    public readonly string[] dayOfWeeks =
    {
        "��", "ȭ", "��", "��", "��", "��", "��"
    };

    //�ð� �������� �ٲ� ������ ������ Action
    public Action<int> Add_Min = add_min => { };
    public Action<int> Add_Hour = add_hour => { };
    public Action<int> Add_Day = add_day => { };
    public Action<int> Add_Month = add_month => { };

    [Header("��¥, �ð��� ���� ������")]
    //��, �ð�, ��, ��, ������ ���� ������ �ʿ���
    public int now_min;
    public int now_hour;
    public int now_day;
    public int now_month;
    public string now_dayOfWeek;

    private int dayOfWeekIndex;

    [Header("��¥, �ð� �ؽ�Ʈ")]
    public Text timeText;
    public Text dateText;

    [Header("����׿� ��� ����")]
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

    //�ð��� ��� �����ִ� �Լ�
    private IEnumerator TimeLogic()
    {
        while(true)
        {
            //���ӽð� 1�и��� �ڷ�ƾ�� ����
            yield return new WaitForSeconds(ONE_MIN_SEC * multifilcation);

            //���ӽð� 1�и��� 1���� �����ش�
            now_min++;

            //1���� ���Ҷ����� �Լ� ȣ��
            Add_Min(1);

            if (now_min >= NEED_MIN)
            {
                //�ѽð��� �����ְ� ���� ���� �ʱ�ȭ
                now_hour++;
                now_min = 0;

                //1�ð��� ���Ҷ� ���� �Լ� ȣ��
                Add_Hour(1);

                if (now_hour >= NEED_HOUR)
                {
                    //�Ϸ縦 �����ְ� ���� �ð��� �ʱ�ȭ
                    now_day++;
                    now_hour = 0;

                    now_dayOfWeek = dayOfWeeks[++dayOfWeekIndex % dayOfWeeks.Length];

                    //1���� ���Ҷ� ���� �Լ� ȣ��
                    Add_Day(1);

                    if (now_day > NEED_DAY)
                    {
                        //�Ѵ��� �����ְ� ���� ��¥�� �ʱ�ȭ
                        now_month++;
                        //0���� �����ϱ� 1�Ϸ� �ʱ�ȭ
                        now_day = 1;

                        //1���� ���Ҷ� ���� �Լ� ȣ��
                        Add_Month(1);
                    }
                }
            }

            //������ ���� �� UI ������Ʈ
            UpdateText();
        }
    }

    public void UpdateText()
    {
        timeText.text = string.Concat(now_hour.ToString("00"), ":", now_min.ToString("00"));
        dateText.text = string.Concat(now_month.ToString("00"), "�� ", now_day.ToString("00"), "�� ", now_dayOfWeek, "����");
    }
}
