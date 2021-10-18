using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StateUI : MonoBehaviour
{
    public Text cName;
    public Image cImage;
    public List<Image> stateImages;
    public List<Slider> stateSliders;
    public float hp;
    public float mp;
    public float attackTime;

    public void Start()
    {
        DOTween.To(() => stateSliders[2].value, x => stateSliders[2].value = x, 1,attackTime).SetEase(Ease.Linear);
    }
}