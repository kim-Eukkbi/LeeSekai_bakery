using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    //�� ��ũ��Ʈ�� ���̻� ������� �ʽ��ϴ�
    //������ ���� ���� �۹� �����ϴ� ��ũ��Ʈ

    public static CropManager Instance;

    //�۹� ����Ʈ
    private List<CropTypeSO> cropTypeList;
    //���� ���� �۹�
    public CropTypeSO cropType;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        cropTypeList = new List<CropTypeSO>();
        //SO�� ����Ǿ��ִ� ����Ʈ�� �����
        cropTypeList = Resources.Load<CropTypeListSO>(typeof(CropTypeListSO).Name).cpList;
        cropType = cropTypeList[0];
    }

    private void Update()
    {
        //�۹������� ���⼭ ������
        if (Input.GetKeyDown(KeyCode.Alpha1)) cropType = cropTypeList[0];
        else if (Input.GetKeyDown(KeyCode.Alpha2)) cropType = cropTypeList[1];
        else if (Input.GetKeyDown(KeyCode.Alpha3)) cropType = cropTypeList[2];
        else if (Input.GetKeyDown(KeyCode.Alpha4)) cropType = cropTypeList[3];
        else if (Input.GetKeyDown(KeyCode.Alpha5)) cropType = cropTypeList[4];
    }
}
