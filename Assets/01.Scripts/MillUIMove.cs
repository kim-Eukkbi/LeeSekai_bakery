using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MillUIMove : MonoBehaviour
{
    //�̹������� x��ġ��
    public readonly int[] positions =
    {
        -670, -360, 0, 360, 670
    };
    //�̹������� ũ��
    public readonly float[] scales =
    {
        0.75f, 0.9f, 1f, 0.9f, 0.75f
    };

    //�θ������Ʈ�� ��ġ
    public Transform parent;
    //������ ������
    public MillItem millItemPrefab;

    //������ ���� ����
    public int[] creationOrder = new int[]
    {
        3, 2, 4, 1, 5, 0, 6
    };

    //�� �� �̹����� �ε���
    public int[] sideItemIdx = new int[]
    {
        0, 6
    };

    //���̶�Ű�󿡼��� ��ġ
    private int[] sortIndexs;
    private int[] sortEndIndexes;

    //Dotween�� ��������
    public bool canMove = true;

    //�������� �̹������� ����Ʈ
    public List<MillItem> items = new List<MillItem>();

    public void InitMillUI()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i] = Instantiate(millItemPrefab, parent);
        }
        //�ʱ�ȭ ���ִ� �۾��� ���� �ȳ�����
    }

    /// <summary>
    /// ���ۼ� UI���� ���������� �̵������ִ� �Լ�
    /// </summary>
    public void MoveRight()
    {
        if(canMove)
        {
            StartCoroutine(MoveRightRoutine());
        }
    }

    /// <summary>
    /// ���ۼ� UI���� �������� �̵������ִ� �Լ�
    /// </summary>
    public void MoveLeft()
    {
        if (canMove)
        {
            StartCoroutine(MoveLeftRoutine());
        }
    }

    /// <summary>
    /// �� �� �̹����� �����ϰ� ������ִ� �Լ�
    /// </summary>
    private void SetEndImageInvisible()
    {
        //��� endImage�� �����ϰ�
        for (int i = 0; i < 2; i++)
        {
            Image image = items[sideItemIdx[i]].GetComponent<Image>();
            Color color = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.color = color;

            items[sideItemIdx[i]].transform.localScale = Vector3.zero;
        }
    }

    /// <summary>
    /// ���̶�Ű�信�� �������ִ� �Լ�
    /// </summary>
    private void Sorthierarchy()
    {
        //���̶�Ű�信�� ����
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.SetSiblingIndex(sortIndexs[i]);
        }
    }

    /// <summary>
    /// endImage�� position�� �������ִ� �Լ�
    /// </summary>
    private void SetSideImagePosition()
    {
        items[sideItemIdx[0]].rect.anchoredPosition = new Vector2(-670, items[sideItemIdx[0]].rect.anchoredPosition.y);
        items[sideItemIdx[1]].rect.anchoredPosition = new Vector2(670, items[sideItemIdx[0]].rect.anchoredPosition.y);
    }

    /// <summary>
    /// ���ۼ�UI�� ���������� �̵������ִ� ��ƾ
    /// </summary>
    private IEnumerator MoveRightRoutine()
    {
        //Dotween�� �ȳ������� �������̰�
        canMove = false;

        //��� endImage�� �����ϰ�
        SetEndImageInvisible();

        for (int i = 0; i < items.Count; i++)
        {
            int a = i; //�̻��� Ŭ������ �̷��� ��ߵ�

            if (i == items.Count - 1) //�� �������̶��
            {
                //������ ���� �ִ� image�� �Ⱥ��̰� ���ְ�
                items[a].GetComponent<Image>().DOFade(0, 0.5f);
                items[a].transform.DOScale(0, 0.5f);

                //���ʿ��ִ� endImage�� ������ ���ش�
                items[sideItemIdx[0]].GetComponent<Image>().DOFade(1, 0.5f);
                items[sideItemIdx[0]].transform.DOScale(0.75f, 0.5f);
                continue;
            }

            Vector2 temp = new Vector2(positions[(a + 1) % items.Count], items[a].rect.anchoredPosition.y);

            DOTween.To(() => items[a].rect.anchoredPosition, pos => items[a].rect.anchoredPosition = pos, temp, 0.5f);
            items[a].rect.DOScale(scales[(a + 1) % items.Count], 0.5f);
        }

        //endImage�� �������� �ٽ� �����ش�
        SetSideImagePosition();

        yield return new WaitForSeconds(0.5f);

        //ũ��� ��ġ��� �ٽ� ����Ʈ ����
        List<MillItem> tempList = new List<MillItem>()
        {
            items[4], items[sideItemIdx[0]], items[0], items[1], items[2], items[3], items[sideItemIdx[1]]
        };

        items = tempList;

        //���̶�Ű�信�� ����
        Sorthierarchy();

        //��� �۾��� ���� �� �ٽ� ������ �� ����
        canMove = true;
    }

    /// <summary>
    /// ���ۼ�UI�� �������� �̵������ִ� ��ƾ
    /// </summary>
    private IEnumerator MoveLeftRoutine()
    {
        //Dotween�� �ȳ������� �������̰�
        canMove = false;

        //��� endImage�� �����ϰ�
        SetEndImageInvisible();

        for (int i = 0; i < items.Count; i++)
        {
            int a = i; //�̻��� Ŭ������ �̷��� ��ߵ�

            if (i == 0) //�� ó���̶��
            {
                //���� ���� �ִ� image�� �Ⱥ��̰� ���ְ�
                items[a].GetComponent<Image>().DOFade(0, 0.5f);
                items[a].transform.DOScale(0, 0.5f);

                //�����ʿ��ִ� endImage�� ������ ���ش�
                items[sideItemIdx[1]].GetComponent<Image>().DOFade(1, 0.5f);
                items[sideItemIdx[1]].transform.DOScale(0.75f, 0.5f);
                continue;
            }

            Vector2 temp = new Vector2(positions[(a - 1) % items.Count], items[a].rect.anchoredPosition.y);

            DOTween.To(() => items[a].rect.anchoredPosition, pos => items[a].rect.anchoredPosition = pos, temp, 0.5f);
            items[a].rect.DOScale(scales[(a - 1) % items.Count], 0.5f);
        }

        //endImage�� �������� �ٽ� �����ش�
        SetSideImagePosition();

        yield return new WaitForSeconds(0.5f);

        //ũ��� ��ġ��� �ٽ� ����Ʈ ����
        List<MillItem> tempList = new List<MillItem>()
        {
            items[sideItemIdx[0]], items[1], items[2], items[3], items[4], items[sideItemIdx[1]], items[0]
        };

        items = tempList;

        //���̶�Ű�信�� ����
        Sorthierarchy();

        //��� �۾��� ���� �� �ٽ� ������ �� ����
        canMove = true;
    }
}
