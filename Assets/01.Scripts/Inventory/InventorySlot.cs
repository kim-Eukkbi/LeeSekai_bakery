using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //�κ��丮 ������ ���״�� �κ��丮 ������ �ϳ��� �ް����� ��ũ��Ʈ��

    //��������Ʈ ������
    private SpriteRenderer sr;
    //����ִ��� ��Ÿ���� �ؽ�Ʈ
    [SerializeField]
    private Text countText;

    //� ����������
    public IInventoryItem item;
    //��� �������ִ���
    public int count;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetItem()
    {
        //SetItem�� ���״�� �������� ���Կ� ������ �� ���� ���� �ϸ� �ȴ�
    }
}
