using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NowShowcasePanel
{
    Showcase,
    Select
}

public class ShowcaseManager : MonoBehaviour
{
    public static ShowcaseManager instance;

    [HideInInspector]
    public SelectPanel selectPanel;

    public Button closeButton;

    public CanvasGroup cvsShowcasePanel;
    public CanvasGroup csvSelectPanel;

    [Header("SelectItem ������")]
    public SelectItem selectItemPrefab;
    public Transform selectParent;

    //Instantiate �� ������Ʈ���� ����ִ� ����Ʈ
    public List<SelectItem> selectItems;

    public NowShowcasePanel nowPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        selectPanel = GetComponentInChildren<SelectPanel>();

        Debug.Log("SelectPanel ������");

        CloseSelectPanel();
        nowPanel = NowShowcasePanel.Showcase;
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            switch (nowPanel)
            {
                case NowShowcasePanel.Showcase:
                    CanvasGroup group = GetComponent<CanvasGroup>();
                    group.alpha = 0;
                    group.interactable = false;
                    group.blocksRaycasts = false;

                    InputManager.Instance.isUIOpen = false;

                    break;
                case NowShowcasePanel.Select:
                    CloseSelectPanel();
                    break;
            }
        });
    }

    public void OpenSelectPanel()
    {
        cvsShowcasePanel.alpha = 0;
        cvsShowcasePanel.blocksRaycasts = false;

        csvSelectPanel.alpha = 1;
        csvSelectPanel.blocksRaycasts = true;

        nowPanel = NowShowcasePanel.Select;

        //�߰��� ����� �� �� �ѹ� �� �������� �� �κ��丮�� �ִ� ���� �� �����ͼ� �����ߴ�

        if(selectItems.Count > 0)
        {
            foreach (var item in selectItems)
            {
                Destroy(item.gameObject);
            }

            selectItems.Clear();
        }

        List<InventorySlot> breadSlots = InventoryManager.Instance.GetAllBreadInInvenroty();

        foreach (var slot in breadSlots)
        {
            SelectItem item = Instantiate(selectItemPrefab, selectParent);

            selectItems.Add(item);
            item.Setting(slot.CurrentItem() as BreadSO, slot.CurrentCount());
        }
    }

    public void CloseSelectPanel()
    {
        cvsShowcasePanel.alpha = 1;
        cvsShowcasePanel.blocksRaycasts = true;

        csvSelectPanel.alpha = 0;
        csvSelectPanel.blocksRaycasts = false;

        nowPanel = NowShowcasePanel.Showcase;
    }

    //��ĳ�̽��޴����� selectPanel���� ���õ� ���� �����ϰ� ShowcaseItem�� �־��ش�

    //1. ShowcaseItem�� ������ (�̶� �ڽ��� ���� item���� �Ű������� ����)
    //2. SelectPanel�� ������ (�翬�� showcasePanel�� ��� ������ ��)
    //3. ������ �������� ������ (�̶��� �ڽ��� ���� ������ �Ű������� �����ؾ���)
    //4. Ȯ�ι�ư�� ������ ������ �������� showcaseItem�� ������ �ٲ��ش�
}
