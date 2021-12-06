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

        //�������� ����Ϸ��� �õ�
        bool tryUse = false;

        //farmTile�� ���ٸ� ����
        if (farmTile == null) return tryUse;

        //�÷��̾� �ȱ��̺��� �ָ� �ִٸ� ����
        if (!player.IsInRange()) return tryUse;

        //�������� �ʴٸ� ����
        if (!farmTile.isWet) return tryUse;

        //�̹� �۹��� �ɰ��� �ִٸ� ����
        if (farmTile.isPlanted) return tryUse;

        //�÷��̾ ������ �� ���� ���¶�� ����
        if (!player.CanAction()) return tryUse;

        //������� �;� �õ� ����
        tryUse = true;

        //���� ������� ������ ���� �� �ִٴ� �Ŵϱ� ����
        farmTile.Plant(cropType);
        //�ִϸ��̼� ������ִ°͵� ���� ����
        player.PlayAnimation(player.PlantName);

        return tryUse;
    }
}
