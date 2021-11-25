using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    //�۹� ����Ʈ
    private List<CropTypeSO> cropTypeList;
    //���� ���� �۹�
    private CropTypeSO cropType;

    private void Awake()
    {
        cropTypeList = new List<CropTypeSO>();
        //SO�� ����Ǿ��ִ� ����Ʈ�� �����
        cropTypeList = Resources.Load<CropTypeListSO>(typeof(CropTypeListSO).Name).cpList;
        cropType = cropTypeList[0];
    }
}
