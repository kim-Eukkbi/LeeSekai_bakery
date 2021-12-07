using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/SeedItemSO")]
public class SeedItemSO : Item
{
    //���� �۹��� ����
    public CropTypeSO cropType;

    public override bool UseItem(RaycastHit hit, PlayerMove player)
    {
        //Ŭ���� ���� �ִ� Ÿ���� �����´�
        FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

        //farmTile�� ���ٸ� ����
        if (farmTile == null) return false;

        //�÷��̾� �ȱ��̺��� �ָ� �ִٸ� ����
        if (!player.IsInRange()) return false;

        //�������� �ʴٸ� ����
        if (!farmTile.isWet) return false;

        //�̹� �۹��� �ɰ��� �ִٸ� ����
        if (farmTile.isPlanted) return false;

        //�÷��̾ ������ �� ���� ���¶�� ����
        if (!player.CanAction()) return false;

        //���� ������� ������ ���� �� �ִٴ� �Ŵϱ� ����
        farmTile.Plant(cropType);
        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.PlantName);

        return true;
    }
}
