using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    //�׷��� �����ɽ��� �ѹ� �Ẹ��
    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;

    //�׷��� �����ɽ��� ������
    private PointerEventData pointer;

    //�÷��̾� ��Ƴ��� ����
    public PlayerMove player;

    [SerializeField]
    private InventorySlot firstSlot;
    [SerializeField]
    private InventorySlot secondSlot;

    private void Awake()
    {
        mainCam = Camera.main;

        pointer = new PointerEventData(eventSystem);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //UI�� Ŭ���Ѱ� �ƴ϶��
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //print(hit.transform.gameObject.name);

                    //���� ���õ� ���� �̾ƿ���
                    InventorySlot inventorySlot = InventoryManager.Instance.NowSelectedInventory();

                    //UseItem�� Harvest �и� ����

                    //���� ���õ� ������ ���� �������� ���̶��
                    if (inventorySlot.CurrentItem() == inventorySlot.handItem)
                    {
                        //��Ȯ�ع���
                        inventorySlot.CurrentItem().UseItem(hit, player);
                    }
                    else
                    {
                        //�ƴ϶�� �״�� ������ ���
                        inventorySlot.UseItem(hit, player);
                    }
                }
            }
            else
            {
                //�ӽ������� ���� ����� �κ��丮 ������ �޾ƿ´�
                InventorySlot tmpSlot = InventoryManager.Instance.FindNearestSlot(pointer, graphicRaycaster);

                //�ִٸ� ���� ù��° ����
                if (tmpSlot != null)
                {
                    firstSlot = tmpSlot;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //�ӽ������� ���� ����� �κ��丮 ������ �޾ƿ´�
                InventorySlot tmpSlot = InventoryManager.Instance.FindNearestSlot(pointer, graphicRaycaster);

                //�ִٸ� ���� �ι�° ����
                if (tmpSlot != null)
                {
                    secondSlot = tmpSlot;
                }

                //�� ������ �ٸ��ٸ� && �Ѵ� null�� �ƴ϶��
                if(firstSlot != secondSlot && firstSlot != null && secondSlot != null)
                {
                    //�ΰ��� �ٲ���
                    InventoryManager.Instance.SwapInventoryItem(firstSlot, secondSlot);
                }
            }
        }
    }
}
