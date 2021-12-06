using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO/ToolItemSO/WateringItemSO")]
public class WateringItemSO : Item
{
    public override bool UseItem(RaycastHit hit, PlayerMove player)
    {
        //클릭한 곳에 있는 타일을 가져온다
        FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

        //아이템을 사용하려고 시도
        bool tryUse = false;

        //farmTile이 없다면 리턴
        if (farmTile == null) return tryUse;

        //플레이어 팔길이보다 멀리 있다면 리턴
        if (!player.IsInRange()) return tryUse;

        //플레이어가 동작할 수 없는 상태라면 리턴
        if (!player.CanAction()) return tryUse;

        //여기까지 와야 시도 성공
        tryUse = true;

        //여기까지 왔으면 물을 줄 수 있으니까 물을 주자
        farmTile.Water(true);

        //애니메이션 재생해주는것도 잊지 말고
        player.PlayAnimation(player.WaterName);

        return tryUse;
    }
}
