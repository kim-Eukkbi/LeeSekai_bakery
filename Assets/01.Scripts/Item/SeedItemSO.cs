using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/SeedItemSO")]
public class SeedItemSO : Item
{
    //���� �۹��� ����
    public CropTypeSO cropType;

    public override void UseItem(RaycastHit hit, PlayerMove player)
    {
        //Ŭ���� ���� �ִ� Ÿ���� �����´�
        FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

        //farmTile�� ���ٸ� ����
        if (farmTile == null) return;

        //�÷��̾� �ȱ��̺��� �ָ� �ִٸ� ����
        if (!player.IsInRange()) return;

        //�������� �ʴٸ� ����
        if (!farmTile.isWet) return;

        //�̹� �۹��� �ɰ��� �ִٸ� ����
        if (farmTile.isPlanted) return;

        //�÷��̾ ������ �� ���� ���¶�� ����
        if (!player.CanAction()) return;

        //���� ������� ������ ���� �� �ִٴ� �Ŵϱ� ����
        farmTile.Plant(cropType);
        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.PlantName);
    }
}
