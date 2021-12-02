using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //�κ��丮 �Ŵ����� �κ��丮�� �Ѱ��ϴ� �༮�̴�
    //���� �������� �����, �κ��丮 ���Գ����� �������� �ٲٴ��� ����� ���ִ� ���̴�

    //�����԰� �κ��丮������ �������ְ�
    public List<InventorySlot> quickSlots;
    public List<InventorySlot> inventorySlots;

    private void Awake()
    {
        //�޸𸮸� �Ҵ�������
        quickSlots = new List<InventorySlot>();
        inventorySlots = new List<InventorySlot>();
    }
}
