using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillUI : MonoBehaviour
{
    //�̹������� ũ��
    private const float bigSize = 1f;
    private const float middleSize = 0.9f;
    private const float smallSize = 0.75f;

    //�̹������� x��ġ��
    private readonly int[] xPostions =
    {
        -670, -360, 0, 360, 670
    };

    //�������� �̹������� ��ġ
    public RectTransform[] imageTrms;
}
