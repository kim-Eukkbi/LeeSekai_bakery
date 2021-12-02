using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/ToolItemSO/WateringItemSO")]
public class WateringItemSO : Item
{
    public override void UseItem(RaycastHit hit, PlayerMove player)
    {
        //Ŭ���� ���� �ִ� Ÿ���� �����´�
        FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

        //farmTile�� ���ٸ� ����
        if (farmTile == null) return;

        //�÷��̾� �ȱ��̺��� �ָ� �ִٸ� ����
        if (!player.IsInRange()) return;

        //�÷��̾ ������ �� ���� ���¶�� ����
        if (!player.CanAction()) return;

        //������� ������ ���� �� �� �����ϱ� ���� ����
        farmTile.Water(true);

        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.WaterName);
    }
}
