using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillItem : MonoBehaviour
{
    [HideInInspector]
    public RectTransform rect;
    public Image breadImage;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
