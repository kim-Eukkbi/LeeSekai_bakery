using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MillCloseBtn : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            transform.parent.gameObject.GetComponent<CanvasGroup>().interactable = false;
            transform.parent.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            InputManager.Instance.isUIOpen = false;
        });
    }
}
