using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //인벤토리 슬롯은 말그대로 인벤토리 슬롯이 하나씩 달고있을 스크립트다

    //아무것도 없을 때 이미지
    public Sprite handSprite;
    public Item handItem;

    //스프라이트 이미지
    public Image image;
    //몇개나있는지 나타나는 텍스트
    public Text countText;

    //어떤 아이템인지
    [SerializeField]
    private Item item;
    //몇개나 가지고있는지
    [SerializeField]
    private int count = 0;

    private void Awake()
    {
        if(item == null)
        {
            //아이템이 없다면 아이템은 손으로 해준다
            item = handItem;
        }

        //UI 한번 업데이트 해주고
        UpdateUI();
    }

    public void SetItem(Item item, int amount)
    {
        //SetItem은 말그대로 아이템이 슬롯에 들어왔을 때 해줄 일을 하면 된다
        //아이템을 바꿔줘
        this.item = item;

        if(item.canNest)
        {
            //중첩 가능한 아이템이라면 갯수만큼 더해준다
            count += amount;
        }

        print($"{this.gameObject.name}, {item.itemName}");

        //일단 UI업데이트 해주고
        UpdateUI();
    }

    public void AddItem(int amount)
    {
        //아이템을 추가한다는건 이미 무슨 아이템이 있는지 알고 추가하는거니까 숫자만 더해주자
        count += amount;

        //UI업데이트하는거 잊지말고
        UpdateUI();
    }

    public Item NowItem()
    {
        //현재 어떤 아이템을 가지고있는지 리턴해준다
        return item;
    }

    public void UseItem(RaycastHit hit, PlayerMove player)
    {
        //아이템 사용에 성공하였는지 여부 (실패했으면 카운트 까면 안댐)
        bool tryUse;

        //UseItem 호출해준다
        tryUse = item.UseItem(hit, player);

        //아이템 사용에 성공했다면
        if(tryUse)
        {
            //아이템이 중첩 가능하다면
            if (item.canNest)
            {
                //하나를 빼주고
                count--;
                //만약에 없다면 손으로 바꿔주고
                if (count <= 0)
                {
                    item = handItem;
                }

                //UI도 업데이트 해준다
                UpdateUI();
            }
        }
    }

    public bool IsEmpty()
    {
        //item이 없으면 true 있으면 false를 반환하겠지? 아니? 그래야만 해
        return item == null || item == handItem;
    }

    public void UpdateUI()
    {
        //아이템이 있는상태라면
        if (item != null)
        {
            //이미지 업데이트 해주고
            image.sprite = item.itemSprite;

            //중첩이 불가능하다면
            if (!item.canNest)
            {
                //숫자는 없어도 됌
                countText.text = string.Empty;
            }
            else
            {
                //중첩이 가능하면 숫자를 넣어줘
                countText.text = count.ToString();
            }
        }
        else
        {
            //없으면 이미지도 없에고 카운트도 없에
            image.sprite = handSprite;
            countText.text = string.Empty;
        }

        //Debug.Log(image.sprite.name);
    }
}
