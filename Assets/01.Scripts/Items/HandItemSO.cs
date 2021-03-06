using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/ToolItemSO/HandSO")]
public class HandItemSO : Item
{
    //이놈은 손이다 말 그대로 
    //일단을 할일이 수확하는 것 밖에 없다

    public override bool UseItem(RaycastHit hit, PlayerMove player)
    {
        //클릭한 곳에 있는 타일을 가져온다
        FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

        //farmTile이 없다면 리턴
        if (farmTile == null) return false;

        //플레이어 팔길이보다 멀리 있다면 리턴
        if (!player.IsInRange()) return false;

        //플레이어가 동작할 수 없는 상태라면 리턴
        if (!player.CanAction()) return false;

        //작물이 심겨져있지 않다면 리턴
        if (!farmTile.isPlanted) return false;

        //작물이 다 자라지 않았다면 리턴
        if (!farmTile.plantedCrop.isGrowEnd) return false;

        //여기까지 왔다면 작물을 수확해주자
        //성공적으로 수확을 했으면 return false;
        farmTile.plantedCrop.Harvest();

        //타일도 리셋해주고
        farmTile.Resetting();

        //애니메이션 재생해주는것도 잊지 말고
        player.PlayAnimation(player.HarvestName);

        return true;
    }
}
