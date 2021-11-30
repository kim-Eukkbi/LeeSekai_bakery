using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Jobs //�÷��̾��� ������
{
    Knights,
    MagicKnight,
    Druid,
    Hunter,
    Priest,
    Elementalist
}

public class Character : MonoBehaviour
{
    public string cName; //ĳ���� �̸�
    public Sprite image; //ĳ���� �̹���
    public float attackTime; //ĳ���� �����غ� �������� �������� �ð�
    public float hp;//ĳ���� ü��
    public float mp;//ĳ���� ����
    public float attackDamage;//ĳ���� ������ 
    public float defense;//ĳ���� ����
    public StateUI stateUI;//ĳ���� UI
    public MonsterStateUI monsterStateUI; // ���� UI
    public bool isMonster = false; // �� ĳ���Ͱ� �������� �÷��̾����� bool
    public Jobs cJobs; // �� ĳ������ ����

    public void Awake()
    {
        if (isMonster) MonsterInit(); //���Ϳ��� �̴ϼȶ���¡
        else Init();//�÷��̾�� �̴ϼȶ���¡
    }

    public void Init() //�÷��̾�� �̴ϼ� ����¡
    {
        stateUI.cName.text = cName;
        stateUI.cImage.sprite = image;
        stateUI.attackTime = attackTime;
        stateUI.hp = hp;
        stateUI.mp = mp;
        stateUI.attackDamage = attackDamage;
        stateUI.defense = defense;
        stateUI.cJobs = cJobs;
    }

    public void MonsterInit() //�������� �̴ϼȶ���¡
    {
        monsterStateUI.mName.text = cName;
        monsterStateUI.mImage.sprite = image;
        monsterStateUI.attackTime = attackTime;
        monsterStateUI.hp = hp;
        monsterStateUI.mp = mp;
        monsterStateUI.attackDamage = attackDamage;
        monsterStateUI.defense = defense;
    }
}
