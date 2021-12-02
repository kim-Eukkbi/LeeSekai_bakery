using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //�κ��丮 �Ŵ����� �κ��丮�� �Ѱ��ϴ� �༮�̴�
    //���� �������� �����, �κ��丮 ���Գ����� �������� �ٲٴ��� ����� ���ִ� ���̴�

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

    public Item NowSelectedItem()
    {
        return selectedQuickSlot.item;
    }
}
