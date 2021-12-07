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

        //farmTile�� ���ٸ� ����
        if (farmTile == null) return false;

        //�÷��̾� �ȱ��̺��� �ָ� �ִٸ� ����
        if (!player.IsInRange()) return false;

        //�÷��̾ ������ �� ���� ���¶�� ����
        if (!player.CanAction()) return false;

        //�۹��� �ɰ������� �ʴٸ� ����
        if (!farmTile.isPlanted) return false;

        //�۹��� �� �ڶ��� �ʾҴٸ� ����
        if (!farmTile.plantedCrop.isGrowEnd) return false;

        //������� �Դٸ� �۹��� ��Ȯ������
        //���������� ��Ȯ�� ������ return false;
        farmTile.plantedCrop.Harvest();

        //Ÿ�ϵ� �������ְ�
        farmTile.Resetting();

        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.HarvestName);

        return true;
    }
}
