using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string cName;
    public Sprite image;
    public float attackTime;
    public float hp;
    public float mp;
    public float attackDamage;
    public float defense;
    public StateUI stateUI;
    public MonsterStateUI monsterStateUI;
    public bool isMonster = false;
    public bool isMeeleAttack;

    public void Awake()
    {
        if (isMonster) MonsterInit();
        else Init();
    }

    public void Init()
    {
        stateUI.cName.text = cName;
        stateUI.cImage.sprite = image;
        stateUI.attackTime = attackTime;
        stateUI.hp = hp;
        stateUI.mp = mp;
        stateUI.attackDamage = attackDamage;
        stateUI.defense = defense;
        stateUI.isMeeleAttack = isMeeleAttack;
    }

    public void MonsterInit()
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
