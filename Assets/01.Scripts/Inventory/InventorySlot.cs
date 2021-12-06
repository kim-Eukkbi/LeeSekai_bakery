using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //�κ��丮 ������ ���״�� �κ��丮 ������ �ϳ��� �ް����� ��ũ��Ʈ��

    //�ƹ��͵� ���� �� �̹���
    public Sprite nullSprite;
    public Item nullItem;

    //��������Ʈ �̹���
    public Image image;
    //����ִ��� ��Ÿ���� �ؽ�Ʈ
    public Text countText;

    //� ����������
    [SerializeField]
    private Item item;
    //��� �������ִ���
    [SerializeField]
    private int count;

    private void Awake()
    {
        if(item == null)
        {
            //�������� ���ٸ� �������� ������ ���ش�
            item = nullItem;
        }

        //UI �ѹ� ������Ʈ ���ְ�
        UpdateUI();
    }

    public void SetItem(Item item, int amount)
    {
        //SetItem�� ���״�� �������� ���Կ� ������ �� ���� ���� �ϸ� �ȴ�
        //�������� �ٲ���
        this.item = item;

        if(item.canNest)
        {
            //��ø ������ �������̶�� ������ŭ �����ش�
            count += amount;
        }

        //�ϴ� UI������Ʈ ���ְ�
        UpdateUI();
    }

    public void AddItem(int amount)
    {
        //�������� �߰��Ѵٴ°� �̹� ���� �������� �ִ��� �˰� �߰��ϴ°Ŵϱ� ���ڸ� ��������
        count += amount;

        //UI������Ʈ�ϴ°� ��������
        UpdateUI();
    }

    public Item NowItem()
    {
        //���� � �������� �������ִ��� �������ش�
        return item;
    }

    public void UseItem(RaycastHit hit, PlayerMove player)
    {
        //������ ��뿡 �����Ͽ����� ���� (���������� ī��Ʈ ��� �ȴ�)
        bool tryUse;

        //UseItem ȣ�����ش�
        tryUse = item.UseItem(hit, player);

        //�������� ��ø �����ϴٸ� && ������ ��뿡 �����ߴٸ�
        if (item.canNest && tryUse)
        {
            //�ϳ��� ���ְ�
            count--;
            //���࿡ ���ٸ� ������ �ٲ��ְ�
            if (count <= 0)
            {
                item = nullItem;
            }

            //UI�� ������Ʈ ���ش�
            UpdateUI();
        }
    }

    public bool IsEmpty()
    {
        //item�� ������ true ������ false�� ��ȯ�ϰ���? �ƴ�? �׷��߸� ��
        return item == null || item == nullItem;
    }

    public void UpdateUI()
    {
        //�������� �ִ»��¶��
        if (item != null)
        {
            //�̹��� ������Ʈ ���ְ�
            image.sprite = item.itemSprite;

            //��ø�� �Ұ����ϴٸ�
            if (!item.canNest)
            {
                //���ڴ� ��� ��
                countText.text = string.Empty;
            }
            else
            {
                //��ø�� �����ϸ� ���ڸ� �־���
                countText.text = count.ToString();
            }
        }
        else
        {
            //������ �̹����� ������ ī��Ʈ�� ����
            image.sprite = nullSprite;
            countText.text = string.Empty;
        }
    }
}
