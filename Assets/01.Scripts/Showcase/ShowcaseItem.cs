using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItem : MonoBehaviour
{
    private Button button;
    public Bread bread; //나중에 이빵을 selectPanel에서 바꿔줘야함 Action을 쓸까?

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            SelectPanel.instance.openPanel(this);
        });
    }
}
