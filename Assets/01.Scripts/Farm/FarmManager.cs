using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    //���⼭�� �� ���ֳ�
    //�ϴ� Ŭ���� ���� ������� ������
    //���� �ѷ��������� �۹��� �ɾ�
    //���� �Ȼѷ��������� ���� ��
    //12345�� �۹��� ����
    //�۹��� CropManager���� �������°ɷ�

    //����ķ ��Ƴ��� ����
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

                //Ŭ���� Ÿ���� �����´�
                FarmTile farmTile = hit.transform.GetComponent<FarmTile>();

                if(farmTile != null)
                {
                    //�����ִ»��¶��
                    if(farmTile.isWet)
                    {
                        //�ƹ� �۹��� �ɰ������� �ʴٸ�
                        if(!farmTile.isPlanted)
                        {
                            //���� ���õǾ��ִ� �۹��� �ɴ´�
                            farmTile.Plant(CropManager.Instance.cropType);
                        }
                    }
                    //�������� ���� ���¶��
                    else
                    {
                        farmTile.Water(true);
                    }
                }
            }
        }
    }
}
