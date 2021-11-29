using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public const float ONE_DAY_SEC = 1440f;

    private CropTypeSO cropType;
    private SpriteRenderer sr;

    //성장 시간
    public float growTime;
    //성장 분기
    public float growQuarter;
    public float[] growPoionts = new float[3];

    public int pointIdx = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cropType = GetComponent<CropTypeHolder>().cropType;

        sr.sprite = cropType.growSprite[0];

        //자라는 시간은 자라는데 걸리는 시간 * 1440초(게임에서의 24시간)
        cropType.growTime = cropType.growDay * ONE_DAY_SEC;

        //변수 초기화하고
        growTime = 0;
        //스프라이트 변화는 3번이니까 3개 분기로 나눠
        growQuarter = growTime / 3;

        float temp = 0;

        for (int i = 0; i < growPoionts.Length; i++)
        {
            //임시 변수에 변화량을 계속 더해준다
            temp += growQuarter;
            //한번 더해준값은 그대로 넣어놔
            growPoionts[i] = temp;
        }
    }

    private void Update()
    {
        //시간없으니까 100배속 가보자
        growTime += Time.deltaTime * 100;

        if(growTime >= growPoionts[pointIdx])
        {
            pointIdx++;
            sr.sprite = cropType.growSprite[pointIdx];
        }
    }
}
