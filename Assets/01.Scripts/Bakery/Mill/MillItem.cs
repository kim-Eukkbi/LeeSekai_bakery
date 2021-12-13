using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillItem : MonoBehaviour
{
    //�������� UI��
    public RectTransform rect;
    public Image breadImage;
    public Image backGroundImage;

    //�̳��� ������ �ִ� ������
    public RecipeSO recipe;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void UpdateUI(RecipeSO recipe)
    {
        //������ �ٲ��ְ�
        this.recipe = recipe;
        //UI ������Ʈ
        breadImage.sprite = recipe.bread.itemSprite;
    }
}
