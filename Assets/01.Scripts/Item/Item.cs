using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    //�κ��丮 �������� �κ��丮�� �� �� �ִ� ��� �������� ��ӹ����� �ȴ�
    //�ʿ��� �͵�
    //�������� ��� ���� �� Ŭ���ϸ� ����Ǵ� �Լ�

    //������ ���� �� �̸��� ���;� �Ǵϱ� �̸��� �־�����
    public string itemName;
    //�κ��丮�� ������ ���̴� ��������Ʈ
    public Sprite itemSprite;
    //������ ��ø ����
    public bool canNest = true;

    //Ŭ�� �� � ��ü�� ��Ҵ��� �˱� ���� RaycastHit�� ������
    //�÷��̾ üũ�ؾ� �ϴ� �����۵� �����Ƿ� �÷��̾ ������
    public abstract void UseItem(RaycastHit hit, PlayerMove player);
}
