using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //�κ��丮 �Ŵ����� �κ��丮�� �Ѱ��ϴ� �༮�̴�
    //���� �������� �����, �κ��丮 ���Գ����� �������� �ٲٴ��� ����� ���ִ� ���̴�

    //���� �ؾ��� �� �������� �߰��ϸ� ����մ� ������ ã�Ƽ� �Ҵ����ִ°� �ϴ� �����ߵɵ�

    //�κ��丮���� ������� �θ������Ʈ�� ��������
    [SerializeField]
    private GameObject quickSlockParent;
    [SerializeField]
    private GameObject inventorySlockParent;

    //���� ���õ� �κ��丮
    [SerializeField]
    private InventorySlot selectedQuickSlot;
    //�κ��丮�� ���õǾ��� �� ���� ������ ������Ʈ
    [SerializeField]
    private RectTransform selectedInventoryTrm;

    //���õ� �κ��丮 ������Ʈ�� ���ں��� ��ġ�� x��(������ y���� �����ϱ�)
    [SerializeField]
    private float[] selectedInventoryXPoints;
    //������ ���� �� ���� y�� ����
    [SerializeField]
    private float selectedInventoryYPoint;

    //�����԰� �κ��丮������ �������ְ�
    [SerializeField]
    private List<InventorySlot> quickSlots;
    [SerializeField]
    private List<InventorySlot> inventorySlots;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //�޸𸮸� �Ҵ�������
        quickSlots = new List<InventorySlot>();
        inventorySlots = new List<InventorySlot>();

        //�θ��� �ڽĿ�����Ʈ���� ������Ʈ�� ����Ʈ�� �ܾ�´�
        quickSlots = quickSlockParent.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlots = inventorySlockParent.GetComponentsInChildren<InventorySlot>().ToList();

        for (int i = 0; i < quickSlots.Count; i++)
        {
            int a = i;

            quickSlots[a].GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                SelectInventory(a);
            });
        }

        //�����Ҷ� 1���� �����ѻ��·�
        SelectInventory(0);
    }

    private void Update()
    {
        //���� Ű�� ������ �׿� �ش��ϴ� �κ��丮�� �������ش�
        //�Լ��� �����ϴ� ���� Ű��ȣ�� �ƴ� ����Ʈ������ ��ȣ
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectInventory(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectInventory(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectInventory(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectInventory(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectInventory(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SelectInventory(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectInventory(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SelectInventory(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) SelectInventory(8);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SelectInventory(9);
    }

    private void SelectInventory(int idx)
    {
        //���õȽ��� �ٲ��ְ�
        selectedQuickSlot = quickSlots[idx];
        //�ð������� ������Ʈ�� �Ű��ش�
        selectedInventoryTrm.anchoredPosition = new Vector2(selectedInventoryXPoints[idx], selectedInventoryYPoint);
    }

    public void SwapInventoryItem(InventorySlot firstSlot, InventorySlot secondSlot)
    {
        //ù��° ���� �����س���
        Item firstItem = firstSlot.CurrentItem();
        int firstCount = firstSlot.CurrentCount();

        //�ΰ� �������ش�
        firstSlot.SetItem(secondSlot.CurrentItem(), secondSlot.CurrentCount());
        secondSlot.SetItem(firstItem, firstCount);
    }

    public InventorySlot NowSelectedInventory()
    {
        //���õǾ��ִ� �������� ����
        return selectedQuickSlot;
    }

    public InventorySlot FindNearestSlot(PointerEventData pointer, GraphicRaycaster graphicRaycaster)
    {
        //Ŭ�� �������� ���� ����� �κ��丮 ������ �������ش�

        //�ӽ����� ����
        InventorySlot tmpSlot = null;

        //������ ��ġ�� �Ű��ش�
        pointer.position = Input.mousePosition;

        //�����Ϳ��� ���� ����� ������� ����Ʈ�� ����ش�
        List<RaycastResult> reslut = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointer, reslut);

        //������ ���� ���ؾߵǴϱ� ���ǽ� ���ְ�
        if (reslut.Count > 0)
        {
            //���� �������� ������Ʈ�� ��������
            tmpSlot = reslut[0].gameObject.transform.parent.GetComponent<InventorySlot>();
        }

        //������ ��������
        return tmpSlot;
    }

    public void AddItem(Item item, int amount = 1)
    {
        //�� �Լ��� �������� �޾Ƽ� ����ִ� �κ��丮�� �־��ִ� �Լ��̴�
        //�־��ִ� ������ �����Ժ��� ä�� �� �κ��丮������ ���������� ä�� �����̴�

        //����ִ� ������ ã������ �̹� ���� �����۽����� �ִ��� ã�ƺ�
        InventorySlot slot = FindSameItemSlot(item);

        if(slot != null)
        {
            //���� �����۽����� ã�Ҵٸ� ������ŭ �����ְ� ����
            slot.AddItem(amount);
            return;
        }

        //���� ������ ������ �����ϱ� �󽽷��� ã�ƺ���
        slot = FindEmptySlot();

        if(slot != null)
        {
            //�� ������ ã�Ҵٸ� ������ ������ŭ �־��ְ� ����
            slot.SetItem(item, amount);

            return;
        }

        //������� �Դٴ°� �κ��丮�� ��á�ٴ� �Ŵ�
        print("�κ��丮�� �� á���ϴ�");

        //���� SelectedSlot���� Harvest�� ȣ���ϸ� ��Ȯ�� ������ ���װ� �ִ�
        //UpdateUI ������ ���� �ִ°� �˰ڴµ� �װ� ����?
    }

    public InventorySlot FindEmptySlot()
    {
        InventorySlot emptySlot = null;

        //������ �߿� ����ִ� ������ ã��
        foreach (InventorySlot slot in quickSlots)
        {
            //����ִ� �ַ��� ã�Ƽ� empty���Կ� �Ҵ�����
            if (slot.IsEmpty())
            {
                emptySlot = slot;
                break;
            }
        }

        if (emptySlot == null)
        {
            //�������� �ٵ��Ҵµ��� ������ �κ��丮 ������ ������
            foreach (InventorySlot slot in inventorySlots)
            {
                //����ִ� �ַ��� ã�Ƽ� empty���Կ� �Ҵ�����
                if (slot.IsEmpty())
                {
                    emptySlot = slot;
                    break;
                }
            }
        }

        //������� �Դµ� ������ �κ��丮 ������ ������
        return emptySlot;
    }

    public InventorySlot FindSameItemSlot(Item item)
    {
        //item�� ������ �ִ� ������ �̹� ������� ������ ã���ش�

        InventorySlot sameItemSlot = null;

        //�����Ժ��� ��������
        foreach (InventorySlot slot in quickSlots)
        {
            if(slot.CurrentItem() == item)
            {
                //���Կ��ִ� �����۰� �������� ���ٸ� �극��ũ
                sameItemSlot = slot;
                break;
            }
        }

        //�������� �����µ��� ���� �׷���?
        if(sameItemSlot == null)
        {
            //�κ��丮 ���Ե� ������ �ѹ�
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.CurrentItem() == item)
                {
                    //���Կ��ִ� �����۰� �������� ���ٸ� �극��ũ
                    sameItemSlot = slot;
                    break;
                }
            }
        }

        //������� �Դµ��� null�̸� ������ ���°Ŵ�
        return sameItemSlot;
    }

    public List<InventorySlot> GetAllBreadInInvenroty()
    {
        List<InventorySlot> breadSlots = new List<InventorySlot>();

        //��� ���� ã��

        foreach (var slot in quickSlots)
        {
            if(slot.CurrentItem() != null)
            {
                if (slot.CurrentItem().isBread)
                {
                    breadSlots.Add(slot);
                }
            }
        }

        foreach (var slot in inventorySlots)
        {
            if (slot.CurrentItem() != null)
            {
                if (slot.CurrentItem().isBread)
                {
                    breadSlots.Add(slot);
                }
            }
        }

        return breadSlots;
    }
}
