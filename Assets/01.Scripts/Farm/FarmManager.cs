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

    //메인캠 담아놓는 변수
    private Camera mainCam;

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
                print(hit.transform.gameObject.name);

                //클릭한 타일을 가져온다
                FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

                if(farmTile != null)
                {
                    //젖어있는상태라면
                    if(farmTile.isWet)
                    {
                        //아무 작물도 심겨져있지 않다면
                        if(!farmTile.isPlanted)
                        {
                            //현재 선택되어있는 작물을 심는다
                            farmTile.Plant(CropManager.Instance.cropType);
                        }
                    }
                    //젖어있지 않은 상태라면
                    else
                    {
                        farmTile.Water(true);
                    }
                }
            }
        }
    }
}
