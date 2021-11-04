using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    private Bread selectedBread;
    [SerializeField]
    public Button confirmBtn;

    private ShowcaseItem nowItem;

    private void Start()
    {
        //전시하기 버튼이 눌렸을 때
        confirmBtn.onClick.AddListener(() =>
        {
            nowItem.bread = selectedBread;
            ShowcaseManager.instance.CloseSelectPanel();
        });
    }

    /// <summary>
    /// 어떤 ShowcaseItem이 열렸는지 알기 위한 함수입니다
    /// </summary>
    /// <param name="item">열린 ShowcaseItem</param>
    public void SetShowcaseItem(ShowcaseItem item)
    {
        nowItem = item;
    }

    /// <summary>
    /// 선택된 빵을 알기 위한 함수입니다
    /// </summary>
    /// <param name="bread">선택된 빵</param>
    public void SelectBread(Bread bread)
    {
        selectedBread = bread;
    }
}
