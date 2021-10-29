using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItem : MonoBehaviour
{
    public Bread bread;
    public Button button;

    private ShowcaseManager sm;
    private SelectPanel selectPanel;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        sm = ShowcaseManager.instance;
        selectPanel = sm.selectPanel;

        button.onClick.AddListener(() =>
        {
            selectPanel.SetShowcaseItem(this);
            sm.OpenSelectPanel();
        });
    }

    public void SetBread(Bread bread)
    {
        this.bread = bread;
    }
}
