using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public Bread bread;
    private Button button;

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
    public void SetBread(Bread bread)
    {
        this.bread = bread;
    }
}