using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //인벤토리 매니저는 인벤토리를 총괄하는 녀석이다
    //지금 퀵슬롯이 어딘지, 인벤토리 슬롯끼리의 아이템을 바꾸던지 등등을 해주는 곳이다

    //퀵슬롯과 인벤토리슬롯을 선언해주고
    public List<InventorySlot> quickSlots;
    public List<InventorySlot> inventorySlots;

    private void Awake()
    {
        //메모리를 할당해주자
        quickSlots = new List<InventorySlot>();
        inventorySlots = new List<InventorySlot>();
    }
}
