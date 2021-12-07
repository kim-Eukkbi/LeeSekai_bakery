using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    //���۹� ������ ����ִ� ScriptableObject
    private CropTypeSO cropType;
    //��������Ʈ ������
    private SpriteRenderer sr;

    //���� �ϴµ� �ɸ��� �ð�
    public float growTime;
    //���� �帥 �ð�
    public float now_growTime;

    //���� �б�
    public float growQuarter;
    //���ʸ��� �ڶ���� �б���
    public float[] growPoionts = new float[5];

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

        //���� �ʱ�ȭ�ϰ�
        now_growTime = 0;

        //�ڶ�� �ð��� �ڶ�µ� �ɸ��� �ð� * (���ӿ����� 24�ð�)
        growTime = cropType.growDay * TimeManager.ONE_DAY_SEC;
        //������ ������ �ٲܲ��ϱ� ������ �����ش�
        growTime /= TimeManager.ONE_MIN_SEC;

        //��������Ʈ ��ȭ�� 4���̴ϱ� list.Count - 1�� ������
        growQuarter = growTime / 4;

        float temp = 0;

        for (int i = 0; i < 4; i++)
        {
            //�ӽ� ������ ��ȭ���� ��� �����ش�
            temp += growQuarter;
            //�ѹ� �����ذ��� �״�� �־��
            growPoionts[i] = temp;
        }
    }

    private void Start()
    {
        //���� �ٲ𶧸��� ȣ��Ǵ� �Լ��� �����Ѵ�
        TimeManager.Instance.Add_Min += add_min =>
        {
            //������ ������ �������ְ�
            if (isGrowEnd) return;

            //�ð� ��ȭ���� ��� �����ش�
            now_growTime += add_min;

            if (now_growTime >= growPoionts[pointIdx])
            {
                //�̷��� �ѹ� �ڶ�����
                pointIdx++;
                sr.sprite = cropType.growSprite[pointIdx];
            }

            //4�� �ڶ�� ���� ����
            if (pointIdx == 4)
            {
                isGrowEnd = true;
            }
        };
    }

    public void Harvest()
    {
        //�� �̷��� �κ��丮�� ��������� �������� �߰��� �ָ� �ǰ���?
        InventoryManager.Instance.AddItem(cropType.harvestItem);

        //�� ��Ȯ�ߴ����� �˷��ָ� ������?
        //print(gameObject.name);

        //�ϴ� ������ ���ֱ⸸ ����
        Destroy(this.gameObject);
    }
}
