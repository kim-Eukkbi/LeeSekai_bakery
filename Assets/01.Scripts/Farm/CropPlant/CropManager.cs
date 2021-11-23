using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public static CropManager Instance;

    //����ķ
    private Camera mainCam;

    //�۹� ����Ʈ
    [SerializeField]
    private List<CropTypeSO> cropTypeList;
    //���� ���� �۹�
    private CropTypeSO cropType;


    private void Awake()
    {
        if (Instance == null) Instance = this;

        mainCam = Camera.main;

        cropTypeList = new List<CropTypeSO>();
        //SO�� ����Ǿ��ִ� ����Ʈ�� �����
        cropTypeList = Resources.Load<CropTypeListSO>(typeof(CropTypeListSO).Name).cpList;
        cropType = cropTypeList[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //����׿� �ڵ�
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
