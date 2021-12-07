using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //�κ��丮 ������ ���״�� �κ��丮 ������ �ϳ��� �ް����� ��ũ��Ʈ��

    //�ƹ��͵� ���� �� �̹���
    public Sprite handSprite;
    public Item handItem;

    //��������Ʈ �̹���
    public Image image;
    //����ִ��� ��Ÿ���� �ؽ�Ʈ
    public Text countText;

    //� ����������
    [SerializeField]
    private Item item;
    //��� �������ִ���
    [SerializeField]
    private int count = 0;

    private void Awake()
    {
        if(item == null)
        {
            //�������� ���ٸ� �������� ������ ���ش�
            item = handItem;
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

        print($"{this.gameObject.name}, {item.itemName}");

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

        //������ ��뿡 �����ߴٸ�
        if(tryUse)
        {
            //�������� ��ø �����ϴٸ�
            if (item.canNest)
            {
                //�ϳ��� ���ְ�
                count--;
                //���࿡ ���ٸ� ������ �ٲ��ְ�
                if (count <= 0)
                {
                    item = handItem;
                }

                //UI�� ������Ʈ ���ش�
                UpdateUI();
            }
        }
    }

    public bool IsEmpty()
    {
        //item�� ������ true ������ false�� ��ȯ�ϰ���? �ƴ�? �׷��߸� ��
        return item == null || item == handItem;
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
            image.sprite = handSprite;
            countText.text = string.Empty;
        }

        //Debug.Log(image.sprite.name);
    }
}
