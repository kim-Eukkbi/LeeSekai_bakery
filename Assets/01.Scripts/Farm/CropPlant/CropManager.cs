using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    //이 스크립트는 더이상 사용하지 않습니다
    //씨앗이 없던 시절 작물 선택하던 스크립트

    public static CropManager Instance;

    //작물 리스트
    private List<CropTypeSO> cropTypeList;
    //현재 심을 작물
    public CropTypeSO cropType;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        cropTypeList = new List<CropTypeSO>();
        //SO에 저장되어있는 리스트로 덮어쓴다
        cropTypeList = Resources.Load<CropTypeListSO>(typeof(CropTypeListSO).Name).cpList;
        cropType = cropTypeList[0];
    }

    private void Update()
    {
        //작물선택은 여기서 해주자
        if (Input.GetKeyDown(KeyCode.Alpha1)) cropType = cropTypeList[0];
        else if (Input.GetKeyDown(KeyCode.Alpha2)) cropType = cropTypeList[1];
        else if (Input.GetKeyDown(KeyCode.Alpha3)) cropType = cropTypeList[2];
        else if (Input.GetKeyDown(KeyCode.Alpha4)) cropType = cropTypeList[3];
        else if (Input.GetKeyDown(KeyCode.Alpha5)) cropType = cropTypeList[4];
    }
}
