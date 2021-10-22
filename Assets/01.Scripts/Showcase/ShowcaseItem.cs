using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItem : MonoBehaviour
{
    private Button button;
    public Bread bread; //���߿� �̻��� selectPanel���� �ٲ������ Action�� ����?

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            SelectPanel.instance.openPanel(this);
        });
    }
}
