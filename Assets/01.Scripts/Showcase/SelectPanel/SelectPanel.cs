using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    public static SelectPanel instance;

    [SerializeField]
    private CanvasGroup showcasePanel;
    private CanvasGroup selectPanel;

    public Action<ShowcaseItem> openPanel = item => { };

    private ShowcaseItem nowItem;
    private Bread selectedBread;

    private void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        openPanel += item =>
        {
            showcasePanel.alpha = 0f;
            selectPanel.alpha = 1f;

            nowItem = item;
        };
    }

    public void SelectBread(Bread bread)
    {
        selectedBread = bread;
    }
}
