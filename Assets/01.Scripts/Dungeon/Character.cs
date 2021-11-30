using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Jobs //플레이어의 직업들
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
    public string cName; //캐릭터 이름
    public Sprite image; //캐릭터 이미지
    public float attackTime; //캐릭터 전투준비 게이지가 차오르는 시간
    public float hp;//캐릭터 체력
    public float mp;//캐릭터 마력
    public float attackDamage;//캐릭터 데미지 
    public float defense;//캐릭터 방어력
    public StateUI stateUI;//캐릭터 UI
    public MonsterStateUI monsterStateUI; // 몬스터 UI
    public bool isMonster = false; // 이 캐릭터가 몬스터인지 플레이어인지 bool
    public Jobs cJobs; // 이 캐릭터의 직업

    public void Awake()
    {
        if (isMonster) MonsterInit(); //몬스터에게 이니셜라이징
        else Init();//플레이어에게 이니셜라이징
    }

    public void Init() //플레이어에게 이니셜 라이징
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

    public void MonsterInit() //몬스터한테 이니셜라이징
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
