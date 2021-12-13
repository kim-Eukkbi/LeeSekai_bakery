using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientItem : MonoBehaviour
{
    public Image image;
    public Text nameText;
    public Text amountText;

    public void UpdateUI(IngredientSO ingredient, int amount = 0)
    {
        //UI 업데이트 해주자
        image.sprite = ingredient.itemSprite;
        nameText.text = ingredient.itemName;

        //갯수는 받아서 해주자
        amountText.text = $"{amount} / 1";
    }
}
