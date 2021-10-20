using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItem : MonoBehaviour
{
    [HideInInspector]
    public Button button;
    public Bread bread;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
}
