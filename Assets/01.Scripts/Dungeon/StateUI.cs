using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StateUI : MonoBehaviour //이니셜라이즈로 받아오는 수치들
{
    public Text cName;
    public Image cImage;
    public List<Image> stateImages;
    public List<Slider> stateSliders;
    public float hp;
    public float mp;
    public float attackDamage;
    public float defense;
    public float attackTime;
    public Jobs cJobs;
}
