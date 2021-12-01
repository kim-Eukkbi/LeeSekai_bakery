using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    //�Ϸ簡 �������� ����� �����س��´�
    public const float ONE_DAY_SEC = 3600f;

    //���۹� ������ ����ִ� ScriptableObject
    private CropTypeSO cropType;
    //��������Ʈ ������
    private SpriteRenderer sr;

    //���� �ð�
    public float growTime;
    //���� �б�
    public float growQuarter;
    //���ʸ��� �ڶ���� �б���
    public float[] growPoionts = new float[3];

    //���� ���° ��������Ʈ����
    public int pointIdx = 0;

    //������ ��������
    public bool isGrowEnd = false;

    private void Awake()
    {
        //��������Ʈ������ ��������
        sr = GetComponent<SpriteRenderer>();
        //�������ִ� �۹� Ÿ�Ե� �̾ƿ�
        cropType = GetComponent<CropTypeHolder>().cropType;

        //��������Ʈ�� �� ó���ɷ� �������ְ�
        sr.sprite = cropType.growSprite[0];

        //�ڶ�� �ð��� �ڶ�µ� �ɸ��� �ð� * (���ӿ����� 24�ð�)
        cropType.growTime = cropType.growDay * ONE_DAY_SEC;

        //���� �ʱ�ȭ�ϰ�
        growTime = 0;
        //��������Ʈ ��ȭ�� 3���̴ϱ� 3�� �б�� ����
        growQuarter = cropType.growTime / 3;

        float temp = 0;

        for (int i = 0; i < growPoionts.Length; i++)
        {
            //�ӽ� ������ ��ȭ���� ��� �����ش�
            temp += growQuarter;
            //�ѹ� �����ذ��� �״�� �־��
            growPoionts[i] = temp;
        }

        StartCoroutine(GrowLogic());
    }

    IEnumerator GrowLogic()
    {
        while (!isGrowEnd)
        {
            //�ð������ϱ� 100��� ������
            growTime += Time.deltaTime * 1000;

            if (growTime >= growPoionts[pointIdx])
            {
                pointIdx++;
                sr.sprite = cropType.growSprite[pointIdx];
            }

            if(pointIdx > 2)
            {
                isGrowEnd = true;
            }

            yield return null;
        }
    }

    public void Harvest()
    {
        //�� �̷��� �κ��丮�� ��������� �������� �߰��� �ָ� �ǰ���?

        //�� ��Ȯ�ߴ����� �˷��ָ� ������?
        print(gameObject.name);
        //�ϴ� ������ ���ֱ⸸ ����
        Destroy(this.gameObject);
    }
}
