using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    //하루가 몇초인지 상수로 선언해놓는다
    public const float ONE_DAY_SEC = 3600f;

    //농작물 정보가 담겨있는 ScriptableObject
    private CropTypeSO cropType;
    //스프라이트 렌더러
    private SpriteRenderer sr;

    //성장 시간
    public float growTime;
    //성장 분기
    public float growQuarter;
    //몇초마다 자라는지 분기점
    public float[] growPoionts = new float[3];

    //지금 몇번째 스프라이트인지
    public int pointIdx = 0;

    //성장이 끝났는지
    public bool isGrowEnd = false;

    private void Awake()
    {
        //스프라이트렌더러 가져오고
        sr = GetComponent<SpriteRenderer>();
        //가지고있는 작물 타입도 뽑아와
        cropType = GetComponent<CropTypeHolder>().cropType;

        //스프라이트는 맨 처음걸로 설정해주고
        sr.sprite = cropType.growSprite[0];

        //자라는 시간은 자라는데 걸리는 시간 * (게임에서의 24시간)
        cropType.growTime = cropType.growDay * ONE_DAY_SEC;

        //변수 초기화하고
        growTime = 0;
        //스프라이트 변화는 3번이니까 3개 분기로 나눠
        growQuarter = cropType.growTime / 3;

        float temp = 0;

        for (int i = 0; i < growPoionts.Length; i++)
        {
            //임시 변수에 변화량을 계속 더해준다
            temp += growQuarter;
            //한번 더해준값은 그대로 넣어놔
            growPoionts[i] = temp;
        }

        StartCoroutine(GrowLogic());
    }

    IEnumerator GrowLogic()
    {
        while (!isGrowEnd)
        {
            //시간없으니까 100배속 가보자
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
        //뭐 미래에 인벤토리가 만들어지면 아이템을 추가해 주면 되겠죠?

        //뭘 수확했는지도 알려주면 좋겠지?
        print(gameObject.name);
        //일단 지금은 없애기만 하자
        Destroy(this.gameObject);
    }
}
