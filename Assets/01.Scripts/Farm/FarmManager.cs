using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmManager : MonoBehaviour
{
    //���⼭�� �� ���ֳ�
    //�ϴ� Ŭ���� ���� ������� ������
    //���� �ѷ��������� �۹��� �ɾ�
    //���� �Ȼѷ��������� ���� ��
    //12345�� �۹��� ����
    //�۹��� CropManager���� �������°ɷ�

    //������ �׳� �̸��� FarmManager�� InputManager�� �´µ� ����

    //����ķ ��Ƴ��� ����
    private Camera mainCam;

    //�÷��̾� ��Ƴ��� ����
    public PlayerMove player;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //print(hit.transform.gameObject.name);
                
                //���� ���õ� ���� �̾ƿ���
                InventorySlot inventorySlot = InventoryManager.Instance.NowSelectedInventory();

                //UseItem�� Harvest �и� ����

                //���� ���õ� ������ ���� �������� ���̶��
                if(inventorySlot.NowItem() == inventorySlot.handItem)
                {
                    //��Ȯ�ع���
                    inventorySlot.NowItem().UseItem(hit, player);
                }
                else
                {
                    //�ƴ϶�� �״�� ������ ���
                    inventorySlot.UseItem(hit, player);
                }
            }
        }
    }
}
