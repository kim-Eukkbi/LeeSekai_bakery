using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillItem : MonoBehaviour
{
    //�������� UI��
    public RectTransform rect;
    public Image breadImage;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
