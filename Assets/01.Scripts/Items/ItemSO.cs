using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/ItemSO")]
public class ItemSO : Item
{
    public override bool UseItem(RaycastHit hit, PlayerMove player)
    {
        //����� �� ���� �������̴� false�� ����
        return false;
    }
}
