using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season
{
    ALL,
    SPRING,
    SUMMER,
    FALL,
    WINTER
}

[CreateAssetMenu(menuName = "FarmSO/CropTypeSO")]
public class CropTypeSO : ScriptableObject
{
    //이놈은 작물이다
    //이름이 있어야되네 ㅋㅋ
    public string cropName;
    //이건 성장하는 스프라이트들
    public List<Sprite> growSprite;
    //프리팹
    public Transform prefab;
    //성장 가능한 계절
    public Season growSeason;
    //성장하는데 걸리는 날짜
    public int growDay;
    //이놈은 날짜를 시간으로 바꾼 값
    [HideInInspector]
    public int growTime;

    //시간에 따른 스프라이트 변화는 다른곳에서 해줘야 함
}
