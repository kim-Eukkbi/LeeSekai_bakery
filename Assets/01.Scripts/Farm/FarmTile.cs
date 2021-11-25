using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour
{
    //스프라이트들 (0 = 안젖은거, 1 = 젖은거)
    [SerializeField]
    private List<Sprite> farmSprites;

    private SpriteRenderer sr;

    //현재 젖어있는 상태인지
    public bool isWet = false;

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
    public void Water(bool isWet)
    {
        this.isWet = isWet;

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
}
