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
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //print(hit.transform.gameObject.name);

                InventorySlot inventorySlot = InventoryManager.Instance.NowSelectedInventory();
                inventorySlot.UseItem(hit, player);
            }
        }
    }
}
