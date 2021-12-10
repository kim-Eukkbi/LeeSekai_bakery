using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillItem : MonoBehaviour
{
    //아이템의 UI들
    public RectTransform rect;
    public Image breadImage;

    //이놈이 가지고 있는 레시피
    [SerializeField]
    private RecipeSO recipe;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetBread(RecipeSO recipe)
    {
        this.recipe = recipe;
    }
}
