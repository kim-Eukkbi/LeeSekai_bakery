using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //���⼭�� �� ���ֳ�
    //�ϴ� Ŭ���� ���� ������� ������
    //���� �ѷ��������� �۹��� �ɾ�
    //���� �Ȼѷ��������� ���� ��
    //12345�� �۹��� ����
    //�۹��� CropManager���� �������°ɷ�

    //������ �׳� �̸��� FarmManager�� InputManager�� �´µ� ����

    //����ķ ��Ƴ��� ����
    public static InputManager Instance = null;

    private Camera mainCam;

    //�׷��� �����ɽ��� �ѹ� �Ẹ��
    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;

    //�׷��� �����ɽ��� ������
    private PointerEventData pointer;

    //�÷��̾� ��Ƴ��� ����
    public PlayerMove player;

    [Header("����UI��")]
    public CanvasGroup fryUI;
    public CanvasGroup ovenUI;
    public CanvasGroup domaUI;

    private InventorySlot firstSlot;
    private InventorySlot secondSlot;

    public bool isUIOpen = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        mainCam = Camera.main;

        pointer = new PointerEventData(eventSystem);
    }

    void Update()
    {
        if (isUIOpen) return;

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

                    //���� �ΰ� �ʱ�ȭ
                    firstSlot = null;
                    secondSlot = null;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //eŰ�� ������ �ֺ��� �ö��̴��� �����´�
            Collider2D[] cols = Physics2D.OverlapCircleAll(player.transform.position, 1f);

            foreach (var col in cols)
            {
                if(col.gameObject.CompareTag("BakeryHouse"))
                {
                    //���� ����������Ʈ���
                    player.Teleport(player.bakeryTpPos, player.bakeryVcamConfiner);
                    break;
                }
                else if(col.gameObject.CompareTag("Fry"))
                {
                    fryUI.alpha = 1;
                    fryUI.interactable = true;
                    fryUI.blocksRaycasts = true;
                    isUIOpen = true;
                    break;
                }
                else if(col.gameObject.CompareTag("Oven"))
                {
                    ovenUI.alpha = 1;
                    ovenUI.interactable = true;
                    ovenUI.blocksRaycasts = true;
                    isUIOpen = true;
                    break;
                }
                else if(col.gameObject.CompareTag("Doma"))
                {
                    domaUI.alpha = 1;
                    domaUI.interactable = true;
                    domaUI.blocksRaycasts = true;
                    isUIOpen = true;
                    break;
                }
                else if(col.gameObject.CompareTag("HorseCar"))
                {
                    //������ ���� �� ���� ���� �ϸ� ��
                }
            }
        }
    }
}
