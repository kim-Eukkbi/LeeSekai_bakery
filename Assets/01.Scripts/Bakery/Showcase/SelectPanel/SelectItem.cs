using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public BreadSO bread;
    public int count;

    private Button button;

    public Image breadImg;
    public Text nameTxt;
    public Text priceTxt;
    public Text countTxt;

    private SelectPanel selectPanel;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        selectPanel = ShowcaseManager.instance.selectPanel;

        button.onClick.AddListener(() =>
        {
            selectPanel.SelectBread(bread);
        });
    }

    /// <summary>
    /// 빵을 세팅해주는 함수입니다
    /// </summary>
    /// <param name="bread">세팅할 빵</param>
    public void Setting(BreadSO bread, int count)
    {
        this.bread = bread;
        this.count = count;

        UpdateUI();
    }

    public void UpdateUI()
    {
        breadImg.sprite = bread.itemSprite;
        nameTxt.text = bread.itemName;
        priceTxt.text = "10000원";
        countTxt.text = count.ToString();
    }
}
