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
        //UI ������Ʈ ������
        image.sprite = ingredient.itemSprite;
        nameText.text = ingredient.itemName;

        //������ �޾Ƽ� ������
        amountText.text = $"{amount} / 1";
    }
}
