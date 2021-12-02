using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    //인벤토리 아이템은 인벤토리에 들어갈 수 있는 모든 아이템이 상속받으면 된다
    //필요한 것들
    //아이템을 들고 있을 때 클릭하면 실행되는 함수

    //가져다 댔을 때 이름은 나와야 되니까 이름을 넣어주자
    public string itemName { get; set; }
    //이름이랑 같이 나올 설명도 달아줄까?
    public string itemExplanation { get; set; }
    public Sprite itemImage { get; set; }

    public void UseItem(Vector2 clickPos);
}
