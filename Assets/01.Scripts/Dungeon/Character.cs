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

public enum State
{
    Live,
    Ready,
    Dead
}

public class Character : MonoBehaviour
{
    public string cName; //ĳ���� �̸�
    public Sprite image; //ĳ���� �̹���
    public float sp; //ĳ���� �����غ� �������� �������� �ð� // ĳ���� ���ǵ�
    public float hp;//ĳ���� ü��
    public float mp;//ĳ���� ����
    public float str;//ĳ���� �⺻ (����) ������ 
    public float mag;//ĳ���� ���� ������
    public float def;//ĳ���� ����
    public StateUI stateUI;//ĳ���� UI
    public MonsterStateUI monsterStateUI; // ���� UI
    public bool isMonster = false; // �� ĳ���Ͱ� �������� �÷��̾����� bool
    public Jobs cJobs; // �� ĳ������ ����
    public State state; // �� ĳ������ ���� ����
    public Sprite dropItemSprite; //���Ͱ� ����߸��� �������� �̹���
    public int dropItemIndex; //���Ͱ� ����߸��� �������� ����

    public void Awake()
    {
        if (isMonster) MonsterInit(); //���Ϳ��� �̴ϼȶ���¡
        else Init();//�÷��̾�� �̴ϼȶ���¡
    }

    public void Init() //�÷��̾�� �̴ϼ� ����¡
    {
        stateUI.cName.text = cName;
        stateUI.cImage.sprite = image;
        stateUI.sp = sp;
        stateUI.hp = hp;
        stateUI.mp = mp;
        stateUI.str = str;
        stateUI.mag = mag;
        stateUI.def = def;
        stateUI.cJobs = cJobs;
        stateUI.state = state;
    }

    public void MonsterInit() //�������� �̴ϼȶ���¡
    {
        monsterStateUI.mName.text = cName;
        monsterStateUI.mImage.sprite = image;
        monsterStateUI.sp = sp;
        monsterStateUI.hp = hp;
        monsterStateUI.mp = mp;
        monsterStateUI.str = str;
        monsterStateUI.def = def;
        monsterStateUI.state = state;
        monsterStateUI.dropItemSprite = dropItemSprite;
        monsterStateUI.dropItemIndex = dropItemIndex;
    }
}
