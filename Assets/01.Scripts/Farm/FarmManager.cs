using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    //여기서는 뭘 해주냐
    //일단 클릭한 밭이 어떤놈인지 가져와
    //물이 뿌려져있으면 작물을 심어
    //물이 안뿌려져있으면 물을 줘
    //12345를 작물로 하자
    //작물은 CropManager에서 가져오는걸로

    //이제는 그냥 이름만 FarmManager고 InputManager가 맞는듯 ㅇㅇ

    //메인캠 담아놓는 변수
    private Camera mainCam;

    //플레이어 담아놓는 변수
    public PlayerMove player;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //print(hit.transform.gameObject.name);

                Item item = InventoryManager.Instance.NowSelectedItem();
                item.UseItem(hit, player);
            }
        }
    }
}
