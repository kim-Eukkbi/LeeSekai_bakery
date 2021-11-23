using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public static CropManager Instance;

    //메인캠
    private Camera mainCam;

    //작물 리스트
    [SerializeField]
    private List<CropTypeSO> cropTypeList;
    //현재 심을 작물
    private CropTypeSO cropType;


    private void Awake()
    {
        if (Instance == null) Instance = this;

        mainCam = Camera.main;

        cropTypeList = new List<CropTypeSO>();
        //SO에 저장되어있는 리스트로 덮어쓴다
        cropTypeList = Resources.Load<CropTypeListSO>(typeof(CropTypeListSO).Name).cpList;
        cropType = cropTypeList[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //디버그용 코드
            print(cropType.cropName);
            Instantiate(cropType.prefab, GetMouseWorldPos(), Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cropType = cropTypeList[0];
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            cropType = cropTypeList[1];
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            cropType = cropTypeList[2];
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            cropType = cropTypeList[3];
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            cropType = cropTypeList[4];
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        return mouseWorldPos;
    }
}
