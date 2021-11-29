using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public const float ONE_DAY_SEC = 1440f;

    private CropTypeSO cropType;
    private SpriteRenderer sr;

    //���� �ð�
    public float growTime;
    //���� �б�
    public float growQuarter;
    public float[] growPoionts = new float[3];

    public int pointIdx = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cropType = GetComponent<CropTypeHolder>().cropType;

        sr.sprite = cropType.growSprite[0];

        //�ڶ�� �ð��� �ڶ�µ� �ɸ��� �ð� * 1440��(���ӿ����� 24�ð�)
        cropType.growTime = cropType.growDay * ONE_DAY_SEC;

        //���� �ʱ�ȭ�ϰ�
        growTime = 0;
        //��������Ʈ ��ȭ�� 3���̴ϱ� 3�� �б�� ����
        growQuarter = growTime / 3;

        float temp = 0;

        for (int i = 0; i < growPoionts.Length; i++)
        {
            //�ӽ� ������ ��ȭ���� ��� �����ش�
            temp += growQuarter;
            //�ѹ� �����ذ��� �״�� �־��
            growPoionts[i] = temp;
        }
    }

    private void Update()
    {
        //�ð������ϱ� 100��� ������
        growTime += Time.deltaTime * 100;

        if(growTime >= growPoionts[pointIdx])
        {
            pointIdx++;
            sr.sprite = cropType.growSprite[pointIdx];
        }
    }
}
