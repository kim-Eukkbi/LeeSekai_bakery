using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //�κ��丮 ������ ���״�� �κ��丮 ������ �ϳ��� �ް����� ��ũ��Ʈ��

    //�ƹ��͵� ���� �� �̹���
    public Sprite nullSprite;
    public Item handItem;

    //��������Ʈ �̹���
    public Image image;
    //����ִ��� ��Ÿ���� �ؽ�Ʈ
    [SerializeField]
    private Text countText;

    //� ����������
    public Item item;
    //��� �������ִ���
    public int count;

    private void Awake()
    {
        //UI �ѹ� ������Ʈ ���ְ�
        UpdateUI();
    }

    public void SetItem(Item item)
    {
        //SetItem�� ���״�� �������� ���Կ� ������ �� ���� ���� �ϸ� �ȴ�
        //�������� �ٲ���
        this.item = item;

        //�ϴ� UI������Ʈ ���ְ�
        UpdateUI();
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
                item = handItem;
            }

            //UI�� ������Ʈ ���ش�
            UpdateUI();
        }
    }

    public bool IsEmpty()
    {
        //item�� ������ true ������ false�� ��ȯ�ϰ���? �ƴ�? �׷��߸� ��
        return item == null;
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
                //��ø�� �����ϸ� ���ڷ� �ٲٴµ�
                if (count <= 0)
                {
                    //ī��Ʈ�� 0���� �۰ų� ���ٸ� �ؽ�Ʈ�� ������
                    countText.text = string.Empty;
                }
                else
                {
                    countText.text = count.ToString();
                }
            }
        }
        else
        {
            //������ �̹����� ������ ī��Ʈ�� ����
            image.sprite = nullSprite;
            countText.text = string.Empty;

            //�׸��� �������� ������ ���ش�
            item = handItem;
        }
    }
}
