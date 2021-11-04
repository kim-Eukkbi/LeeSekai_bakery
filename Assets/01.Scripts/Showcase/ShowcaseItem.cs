using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItem : MonoBehaviour
{
    public Bread bread;
    public Button button;

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

    public void SetBread(Bread bread)
    {
        this.bread = bread;
    }
}
