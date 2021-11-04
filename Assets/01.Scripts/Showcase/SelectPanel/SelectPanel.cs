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
        //�����ϱ� ��ư�� ������ ��
        confirmBtn.onClick.AddListener(() =>
        {
            nowItem.bread = selectedBread;
            ShowcaseManager.instance.CloseSelectPanel();
        });
    }

    /// <summary>
    /// � ShowcaseItem�� ���ȴ��� �˱� ���� �Լ��Դϴ�
    /// </summary>
    /// <param name="item">���� ShowcaseItem</param>
    public void SetShowcaseItem(ShowcaseItem item)
    {
        nowItem = item;
    }

    /// <summary>
    /// ���õ� ���� �˱� ���� �Լ��Դϴ�
    /// </summary>
    /// <param name="bread">���õ� ��</param>
    public void SelectBread(Bread bread)
    {
        selectedBread = bread;
    }
}
