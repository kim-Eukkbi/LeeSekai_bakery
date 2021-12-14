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
        //�����ϱ� ��ư�� ������ ��
        confirmBtn.onClick.AddListener(() =>
        {
            nowItem.SetBread(selectedBread);
            ShowcaseManager.instance.CloseSelectPanel();

            InventorySlot slot = InventoryManager.Instance.FindSameItemSlot(selectedBread);
            slot.SubItem(1);
        });
    }

    /// <summary>
    /// ���� ShowcaseItem�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="item">���� ShowcaseItem</param>
    public void SetShowcaseItem(ShowcaseItem item)
    {
        nowItem = item;
        breadImg.sprite = nowItem.breadImg.sprite;
        nametxt.text = nowItem.breadName.text;
        priceTxt.text = nowItem.pricetxt.text;
    }

    /// <summary>
    /// ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="bread">���õ� ��</param>
    public void SelectBread(BreadSO bread)
    {
        selectedBread = bread;

        breadImg.sprite = bread.itemSprite;
        nametxt.text = bread.itemName;
    }
}
