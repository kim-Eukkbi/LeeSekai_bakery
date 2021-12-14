using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    //인벤토리 아이템은 인벤토리에 들어갈 수 있는 모든 아이템이 상속받으면 된다
    //필요한 것들
    //아이템을 들고 있을 때 클릭하면 실행되는 함수

    //가져다 댔을 때 이름은 나와야 되니까 이름을 넣어주자
    public string itemName;
    //인벤토리에 들어갔을때 보이는 스프라이트
    public Sprite itemSprite;
    //아이템 중첩 여부
    public bool canNest = true;

    //빵인지 알기 위한 여부
    public bool isBread = false;

    //클릭 시 어떤 물체에 닿았는지 알기 위해 RaycastHit을 보낸다
    //플레이어가 체크해야 하는 아이템도 있으므로 플레이어도 보내자
    //아이템 사용여부를 리턴해야 하니 bool 형식이겠지?
    public abstract bool UseItem(RaycastHit hit, PlayerMove player);
}
