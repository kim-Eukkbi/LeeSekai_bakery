using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillItem : MonoBehaviour
{
    //아이템의 UI들
    public RectTransform rect;
    public Image breadImage;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
