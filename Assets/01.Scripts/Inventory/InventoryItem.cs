using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    //�κ��丮 �������� �κ��丮�� �� �� �ִ� ��� �������� ��ӹ����� �ȴ�
    //�ʿ��� �͵�
    //�������� ��� ���� �� Ŭ���ϸ� ����Ǵ� �Լ�

    //������ ���� �� �̸��� ���;� �Ǵϱ� �̸��� �־�����
    public string itemName { get; set; }
    //�̸��̶� ���� ���� ���� �޾��ٱ�?
    public string itemExplanation { get; set; }
    public Sprite itemImage { get; set; }

    public void UseItem(Vector2 clickPos);
}
