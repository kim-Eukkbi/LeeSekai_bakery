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
    public StateUI stateUI;

    public void Awake()
    {
        stateUI.cName.text = cName;
        stateUI.cImage.sprite = image;
        stateUI.attackTime = attackTime;
        stateUI.hp = hp;
        stateUI.mp = mp;
    }
}
