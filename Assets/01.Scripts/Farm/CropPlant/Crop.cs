using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    private CropTypeSO cropType;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cropType = GetComponent<CropTypeHolder>().cropType;

        sr.sprite = cropType.growSprite[0];
    }
}
