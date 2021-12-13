using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillItem : MonoBehaviour
{
    //아이템의 UI들
    public RectTransform rect;
    public Image breadImage;
    public Image backGroundImage;

    //이놈이 가지고 있는 레시피
    public RecipeSO recipe;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void UpdateUI(RecipeSO recipe)
    {
        //레시피 바꿔주고
        this.recipe = recipe;
        //UI 업데이트
        breadImage.sprite = recipe.bread.itemSprite;
    }
}
