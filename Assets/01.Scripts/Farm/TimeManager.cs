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

    //�׳� ���� ������ �󸶳� �Ǿ��� ���� üũ����
    //���� ������ ���� ���ؼ� �󸶳� �ʿ������� ����
    private const float NEXT_MIN = 2.5f;
    private const int NEXT_HOUR = 60;
    private const int NEXT_DAY = 24;
    private const int NEXT_MONTH = 30;

    private readonly string[] dayOfWeekName =
    {
        "��", "ȭ", "��", "��", "��", "��", "��"
    };

    //������ �ð��� ��� �ʱ�ȭ�ȴ�
    //�׸��� ���� ���� �ð��� ��� ������ �־�߰���
    //Time.deltaTime�� ��� ������ sec ������ �־�߰���? �׳� �ڷ�ƾ ����? �ƴ�? �ϴ� �غ�
    [Header("���� ������ �ð�")]
    public float now_sec = 0;
    public int now_min = 0;
    public int now_hour = 0;

    [Header("���� ������ ��¥")]
    public int now_month = 1;
    public int now_day = 0;
    public string now_dayOfWeek = "��";
    private int now_dayOfWeek_index = 0;

    //������ �ð����ùۿ� �ȸ������
    [Header("�ð�, ��¥ �ؽ�Ʈ")]
    public Text timeText;
    public Text dateText;

    //����� ���ϰ� �ϱ� ���ؼ� ��� ������ �����س���
    [Header("�ð� ��� ����")]
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
        //��� �ð��� ������ �Ǵ� now_sec�� ��Ӻ����� �����ش�
        now_sec += Time.deltaTime * multiplication;

        //��Ϲ���ó�� �׳� 60�� ������ 1�ð� �����ִ� ������ �ٲ���
        if(now_sec >= NEXT_MIN)
        {
            //1���� �����ְ� �ʴ� �ʱ�ȭ
            now_min++;
            now_sec = 0;

            if(now_min >= NEXT_HOUR)
            {
                //1�ð��� �����ְ� ���� �ʱ�ȭ
                now_hour++;
                now_min = 0;

                if(now_hour >= NEXT_DAY)
                {
                    //1���� �����ְ� �ð��� �ʱ�ȭ
                    now_day++;
                    now_hour = 0;

                    //�������Ϸ� �ٲ��ش�
                    now_dayOfWeek = dayOfWeekName[now_dayOfWeek_index % dayOfWeekName.Length + 1];

                    if(now_day > NEXT_MONTH)
                    {
                        //1���� �����ְ� ���� �ʱ�ȭ
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
        dateText.text = string.Concat(now_month.ToString("00"), "�� ", now_day.ToString("00"), "�� ", now_dayOfWeek, "����");
    }
}
