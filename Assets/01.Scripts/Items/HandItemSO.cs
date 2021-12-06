using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/ToolItemSO/HandSO")]
public class HandItemSO : Item
{
    //�̳��� ���̴� �� �״�� 
    //�ϴ��� ������ ��Ȯ�ϴ� �� �ۿ� ����

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

        //�۹��� �ɰ������� �ʴٸ� ����
        if (!farmTile.isPlanted) return tryUse;

        //�۹��� �� �ڶ��� �ʾҴٸ� ����
        if (!farmTile.plantedCrop.isGrowEnd) return tryUse;

        //������� �;� �õ� ����
        tryUse = true;

        //������� �Դٸ� �۹��� ��Ȯ������
        farmTile.plantedCrop.Harvest();
        //Ÿ�ϵ� �������ְ�
        farmTile.Resetting();

        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.HarvestName);

        return tryUse;
    }
}
