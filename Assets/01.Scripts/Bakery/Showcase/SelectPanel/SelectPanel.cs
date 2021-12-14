using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    private BreadSO selectedBread;
    [SerializeField]
    public Button confirmBtn;

    public Image breadImg;
    public Text nametxt;
    public Text priceTxt;

    private ShowcaseItem nowItem;

    private void Start()
    {
        //전시하기 버튼이 눌렸을 때
        confirmBtn.onClick.AddListener(() =>
        {
            nowItem.SetBread(selectedBread);
            ShowcaseManager.instance.CloseSelectPanel();

            InventorySlot slot = InventoryManager.Instance.FindSameItemSlot(selectedBread);
            slot.SubItem(1);
        });
    }

    /// <summary>
    /// 열린 ShowcaseItem을 선택하는 함수
    /// </summary>
    /// <param name="item">열린 ShowcaseItem</param>
    public void SetShowcaseItem(ShowcaseItem item)
    {
        nowItem = item;
        breadImg.sprite = nowItem.breadImg.sprite;
        nametxt.text = nowItem.breadName.text;
        priceTxt.text = nowItem.pricetxt.text;
    }

    /// <summary>
    /// 빵을 선택하는 함수
    /// </summary>
    /// <param name="bread">선택된 빵</param>
    public void SelectBread(BreadSO bread)
    {
        selectedBread = bread;

        breadImg.sprite = bread.itemSprite;
        nametxt.text = bread.itemName;
    }
}
