using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    //농작물 정보가 담겨있는 ScriptableObject
    private CropTypeSO cropType;
    //스프라이트 렌더러
    private SpriteRenderer sr;

    //성장 하는데 걸리는 시간
    public float growTime;
    //현재 흐른 시간
    public float now_growTime;

    //성장 분기
    public float growQuarter;
    //몇초마다 자라는지 분기점
    public float[] growPoionts = new float[5];

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

        //변수 초기화하고
        now_growTime = 0;

        //자라는 시간은 자라는데 걸리는 시간 * (게임에서의 24시간)
        growTime = cropType.growDay * TimeManager.ONE_DAY_SEC;
        //단위를 분으로 바꿀꺼니까 분으로 나눠준다
        growTime /= TimeManager.ONE_MIN_SEC;

        //스프라이트 변화는 4번이니까 list.Count - 1로 나누자
        growQuarter = growTime / 4;

        float temp = 0;

        for (int i = 0; i < 4; i++)
        {
            //임시 변수에 변화량을 계속 더해준다
            temp += growQuarter;
            //한번 더해준값은 그대로 넣어놔
            growPoionts[i] = temp;
        }
    }

    private void Start()
    {
        //분이 바뀔때마다 호출되는 함수를 구독한다
        TimeManager.Instance.Add_Min += add_min =>
        {
            //성장이 끝나면 리턴해주고
            if (isGrowEnd) return;

            //시간 변화량을 계속 더해준다
            now_growTime += add_min;

            if (now_growTime >= growPoionts[pointIdx])
            {
                //이러면 한번 자란거임
                pointIdx++;
                sr.sprite = cropType.growSprite[pointIdx];
            }

            //4번 자라고 나면 리턴
            if (pointIdx == 4)
            {
                isGrowEnd = true;
            }
        };
    }

    public void Harvest()
    {
        //뭐 미래에 인벤토리가 만들어지면 아이템을 추가해 주면 되겠죠?
        InventoryManager.Instance.AddItem(cropType.harvestItem);

        //뭘 수확했는지도 알려주면 좋겠지?
        //print(gameObject.name);

        //일단 지금은 없애기만 하자
        Destroy(this.gameObject);
    }
}
