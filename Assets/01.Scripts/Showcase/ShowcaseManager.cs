using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseManager : MonoBehaviour
{
    public static ShowcaseManager instance;

    public SelectPanel selectPanel;

    public CanvasGroup cvsShowcasePanel;
    public CanvasGroup csvSelectPanel;

    public void OpenSelectPanel()
    {
        cvsShowcasePanel.alpha = 0;
        csvSelectPanel.alpha = 1;
    }

    public void CloseSelectPanel()
    {
        cvsShowcasePanel.alpha = 1;
        csvSelectPanel.alpha = 0;
    }

    private void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }
    }

    //��ĳ�̽��޴����� selectPanel���� ���õ� ���� �����ϰ� ShowcaseItem�� �־��ش�

    //1. ShowcaseItem�� ������ (�̶� �ڽ��� ���� item���� �Ű������� ����)
    //2. SelectPanel�� ������ (�翬�� showcasePanel�� ��� ������ ��)
    //3. ������ �������� ������ (�̶��� �ڽ��� ���� ������ �Ű������� �����ؾ���)
    //4. Ȯ�ι�ư�� ������ ������ �������� showcaseItem�� ������ �ٲ��ش�
}
