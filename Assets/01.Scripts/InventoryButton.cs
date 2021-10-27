using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryButton : MonoBehaviour
{
    private Button btn;
    public RectTransform inventoryPanel;

    private const float BEFORE_Y = -500f;
    private const float AFTER_Y = 150f;

    private const float DURATION = 0.5f;

    private bool isOpen = false;
    private bool canMove = true; //´åÆ®À©ÀÌ ³¡³µ´ÂÁö

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            if (!canMove) return;

            if(isOpen)
            {
                Vector2 nextPos = new Vector2(inventoryPanel.anchoredPosition.x, BEFORE_Y);
                DOTween.To(() => inventoryPanel.anchoredPosition, pos => inventoryPanel.anchoredPosition = pos, nextPos, DURATION)
                .OnComplete(() => canMove = true);
            }
            else
            {
                Vector2 nextPos = new Vector2(inventoryPanel.anchoredPosition.x, AFTER_Y);
                DOTween.To(() => inventoryPanel.anchoredPosition, pos => inventoryPanel.anchoredPosition = pos, nextPos, DURATION)
                .OnComplete(() => canMove = true);
            }

            isOpen = !isOpen;
        });
    }
}
