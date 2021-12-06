using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/ItemSO")]
public class ItemSO : Item
{
    public override bool UseItem(RaycastHit hit, PlayerMove player)
    {
        //사용할 수 없는 아이템이니 false를 리턴
        return false;
    }
}
