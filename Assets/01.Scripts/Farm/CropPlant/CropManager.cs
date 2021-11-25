using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    //작물 리스트
    private List<CropTypeSO> cropTypeList;
    //현재 심을 작물
    private CropTypeSO cropType;

    private void Awake()
    {
        cropTypeList = new List<CropTypeSO>();
        //SO에 저장되어있는 리스트로 덮어쓴다
        cropTypeList = Resources.Load<CropTypeListSO>(typeof(CropTypeListSO).Name).cpList;
        cropType = cropTypeList[0];
    }
}
