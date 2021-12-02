using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //인벤토리 슬롯은 말그대로 인벤토리 슬롯이 하나씩 달고있을 스크립트다

    //아무것도 없을 때 이미지
    public Sprite nullSprite;

    //스프라이트 이미지
    public Image image;
    //몇개나있는지 나타나는 텍스트
    [SerializeField]
    private Text countText;

    //어떤 아이템인지
    public Item item;
    //몇개나 가지고있는지
    public int count;

    private void Awake()
    {
        //아이템이 있는상태라면
        if(item != null)
        {
            image.sprite = item.itemSprite;

            //중첩이 불가능하다면
            if(!item.canNest)
            {
                //숫자는 없어도 됌
                countText.text = string.Empty;
            }
            else
            {
                //가능하면 숫자로 바꿔
                countText.text = count.ToString();
            }
        }
        else
        {
            //없으면 이미지도 없에고 카운트도 없에
            image.sprite = nullSprite;
            countText.text = string.Empty;
        }
    }

    public void SetItem()
    {
        //SetItem은 말그대로 아이템이 슬롯에 들어왔을 때 해줄 일을 하면 된다
    }
}
