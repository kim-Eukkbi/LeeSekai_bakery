using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //�κ��丮 ������ ���״�� �κ��丮 ������ �ϳ��� �ް����� ��ũ��Ʈ��

    //�ƹ��͵� ���� �� �̹���
    public Sprite nullSprite;

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
        //�������� �ִ»��¶��
        if(item != null)
        {
            image.sprite = item.itemSprite;

            //��ø�� �Ұ����ϴٸ�
            if(!item.canNest)
            {
                //���ڴ� ��� ��
                countText.text = string.Empty;
            }
            else
            {
                //�����ϸ� ���ڷ� �ٲ�
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

    public void SetItem()
    {
        //SetItem�� ���״�� �������� ���Կ� ������ �� ���� ���� �ϸ� �ȴ�
    }
}
