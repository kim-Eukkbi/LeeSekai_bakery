using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/ToolItemSO/WateringItemSO")]
public class WateringItemSO : Item
{
    public override bool UseItem(RaycastHit hit, PlayerMove player)
    {
        //Ŭ���� ���� �ִ� Ÿ���� �����´�
        FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

        //farmTile�� ���ٸ� ����
        if (farmTile == null) return false;

        //�÷��̾� �ȱ��̺��� �ָ� �ִٸ� ����
        if (!player.IsInRange()) return false;

        //�÷��̾ ������ �� ���� ���¶�� ����
        if (!player.CanAction()) return false;

        //������� ������ ���� �� �� �����ϱ� ���� ����
        farmTile.Water(true);

        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.WaterName);

        return true;
    }
}
