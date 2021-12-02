using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour
{
    //스프라이트들 (0 = 안젖은거, 1 = 젖은거)
    [SerializeField]
    private List<Sprite> farmSprites;

    //스프라이트 렌더러
    private SpriteRenderer sr;

    //현재 젖어있는 상태인지
    public bool isWet = false;
    //현재 작물이 심겨있는 상태인지
    public bool isPlanted = false;

    //현재 심겨져 있는 작물
    public Crop plantedCrop = null;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        farmSprites = new List<Sprite>();
        farmSprites = Resources.Load<FarmTileSpriteListSO>(typeof(FarmTileSpriteListSO).Name).farmTileSprites;
    }

    //현재 젖어있는 상태인지 확인
    public bool IsWet()
    {
        return isWet;
    }

    //물주는 함수
    public void Water(bool isWatered)
    {
        isWet = isWatered;

        //예가 젖은상태임
        if(isWet)
        {
            sr.sprite = farmSprites[1];
        }
        else
        {
            sr.sprite = farmSprites[0];
        }
    }

    //작물심는 함수
    public void Plant(CropTypeSO crop)
    {
        //이미 작물이 심겨져있다면 리턴
        if (isPlanted) return;

        //작물이 심겼다고 알려줌
        isPlanted = true;
        //현재 위치에 작물을 생성 하면서 변수에 담아두자
        plantedCrop = Instantiate(crop.prefab, transform.position, Quaternion.identity).GetComponent<Crop>();
    }

    public void Resetting()
    {
        isPlanted = false;
        plantedCrop = null;
    }
}
