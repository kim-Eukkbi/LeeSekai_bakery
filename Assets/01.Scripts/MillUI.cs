using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillUI : MonoBehaviour
{
    //이미지들의 크기
    private const float bigSize = 1f;
    private const float middleSize = 0.9f;
    private const float smallSize = 0.75f;

    //이미지들의 x위치값
    private readonly int[] xPostions =
    {
        -670, -360, 0, 360, 670
    };

    //보여지는 이미지들의 위치
    public RectTransform[] imageTrms;
}
