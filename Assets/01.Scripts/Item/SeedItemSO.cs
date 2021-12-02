using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/SeedItemSO")]
public class SeedItemSO : Item
{
    //심을 작물의 종류
    public CropTypeSO cropType;

    public override void UseItem(RaycastHit hit, PlayerMove player)
    {
        //클릭한 곳에 있는 타일을 가져온다
        FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

        //farmTile이 없다면 리턴
        if (farmTile == null) return;

        //플레이어 팔길이보다 멀리 있다면 리턴
        if (!player.IsInRange()) return;

        //젖어있지 않다면 리턴
        if (!farmTile.isWet) return;

        //이미 작물이 심겨져 있다면 리턴
        if (farmTile.isPlanted) return;

        //플레이어가 동작할 수 없는 상태라면 리턴
        if (!player.CanAction()) return;

        //이제 여기까지 왔으면 심을 수 있다는 거니까 심자
        farmTile.Plant(cropType);
        //애니메이션 재생해주는것도 잊지 말고
        player.PlayAnimation(player.PlantName);
    }
}
