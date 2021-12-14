using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItem : MonoBehaviour
{
    [Header("»§ °ü·Ã")]
    public BreadSO bread;

    [Header("UI")]
    public Button button;

    public Image breadImg;
    public Text breadName;
    public Text pricetxt;

    private SelectPanel selectPanel;

    private void Start()
    {
        selectPanel = ShowcaseManager.instance.selectPanel;

        button.onClick.AddListener(() =>
        {
            selectPanel.SetShowcaseItem(this);
            ShowcaseManager.instance.OpenSelectPanel();
        });
    }

    public void SetBread(BreadSO bread)
    {
        this.bread = bread;

        breadImg.sprite = bread.itemSprite;
        breadName.text = bread.itemName;
        pricetxt.text = "10000¿ø";
    }
}
