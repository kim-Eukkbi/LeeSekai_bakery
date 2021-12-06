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

        //�������� ����Ϸ��� �õ�
        bool tryUse = false;

        //farmTile�� ���ٸ� ����
        if (farmTile == null) return tryUse;

        //�÷��̾� �ȱ��̺��� �ָ� �ִٸ� ����
        if (!player.IsInRange()) return tryUse;

        //�÷��̾ ������ �� ���� ���¶�� ����
        if (!player.CanAction()) return tryUse;

        //������� �;� �õ� ����
        tryUse = true;

        //������� ������ ���� �� �� �����ϱ� ���� ����
        farmTile.Water(true);

        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.WaterName);

        return tryUse;
    }
}
