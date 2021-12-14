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
    /// ���� �������ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="bread">������ ��</param>
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
        priceTxt.text = "10000��";
        countTxt.text = count.ToString();
    }
}
