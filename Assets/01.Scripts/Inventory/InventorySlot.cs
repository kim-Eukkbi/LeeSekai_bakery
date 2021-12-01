using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //인벤토리 슬롯은 말그대로 인벤토리 슬롯이 하나씩 달고있을 스크립트다

    //스프라이트 렌더러
    private SpriteRenderer sr;
    //몇개나있는지 나타나는 텍스트
    [SerializeField]
    private Text countText;

    //어떤 아이템인지
    public IInventoryItem item;
    //몇개나 가지고있는지
    public int count;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetItem()
    {
        //SetItem은 말그대로 아이템이 슬롯에 들어왔을 때 해줄 일을 하면 된다
    }
}
