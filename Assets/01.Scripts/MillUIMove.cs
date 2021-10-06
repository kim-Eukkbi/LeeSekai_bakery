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

    //���̶�Ű�󿡼��� ��ġ
    private int[] sortIndexs;
    private int[] sortEndIndexes;

    //Dotween�� ��������
    public bool canMove = true;

    //�������� �̹������� ��ġ
    public List<RectTransform> images = new List<RectTransform>();
    //�� �� �̹�����
    public List<RectTransform> endImages = new List<RectTransform>(); // 0 = Left, 1 = Right

    private void Start()
    {
        //�����ִ� UI�� ������� ��ġ ����
        sortIndexs = new int[]
        {
            images[0].GetSiblingIndex(),
            images[1].GetSiblingIndex(),
            images[2].GetSiblingIndex(),
            images[3].GetSiblingIndex(),
            images[4].GetSiblingIndex()
        };
        sortEndIndexes = new int[]
        {
            endImages[0].GetSiblingIndex(),
            endImages[1].GetSiblingIndex()
        };
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
    /// endImage�� �����ϰ� ������ִ� �Լ�
    /// </summary>
    private void SetEndImageInvisible()
    {
        //��� endImage�� �����ϰ�
        for (int i = 0; i < endImages.Count; i++)
        {
            Image image = endImages[i].GetComponent<Image>();
            Color color = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.color = color;

            endImages[i].localScale = Vector3.zero;
        }
    }

    /// <summary>
    /// ���̶�Ű�信�� �������ִ� �Լ�
    /// </summary>
    private void Sorthierarchy()
    {
        //���̶�Ű�信�� ����
        for (int i = 0; i < 5; i++)
        {
            images[i].SetSiblingIndex(sortIndexs[i]);
        }
        for (int i = 0; i < 2; i++)
        {
            endImages[i].SetSiblingIndex(sortEndIndexes[i]);
        }
    }

    /// <summary>
    /// endImage�� positiond�� �������ִ� �Լ�
    /// </summary>
    private void SetEndImagePosition()
    {
        endImages[0].anchoredPosition = new Vector2(-670, endImages[0].anchoredPosition.y);
        endImages[1].anchoredPosition = new Vector2(670, endImages[0].anchoredPosition.y);
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

        for (int i = 0; i < images.Count; i++)
        {
            int a = i; //�̻��� Ŭ������ �̷��� ��ߵ�

            if (i == images.Count - 1) //�� �������̶��
            {
                //������ ���� �ִ� image�� �Ⱥ��̰� ���ְ�
                images[a].GetComponent<Image>().DOFade(0, 0.5f);
                images[a].DOScale(0, 0.5f);

                //���ʿ��ִ� endImage�� ������ ���ش�
                endImages[0].GetComponent<Image>().DOFade(1, 0.5f);
                endImages[0].DOScale(0.75f, 0.5f);
                continue;
            }

            Vector2 temp = new Vector2(positions[(a + 1) % images.Count], images[a].anchoredPosition.y);

            DOTween.To(() => images[a].anchoredPosition, pos => images[a].anchoredPosition = pos, temp, 0.5f);
            images[a].DOScale(scales[(a + 1) % images.Count], 0.5f);
        }

        //endImage�� �������� �ٽ� �����ش�
        SetEndImagePosition();

        yield return new WaitForSeconds(0.5f);

        //ũ��� ��ġ��� �ٽ� ����Ʈ ����
        List<RectTransform> tempList = new List<RectTransform>()
        {
            endImages[0], images[0], images[1], images[2], images[3]
        };

        List<RectTransform> tempEndList = new List<RectTransform>()
        {
            images[4], endImages[1]
        };

        images = tempList;
        endImages = tempEndList;

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

        for (int i = 0; i < images.Count; i++)
        {
            int a = i; //�̻��� Ŭ������ �̷��� ��ߵ�

            if (i == 0) //�� ó���̶��
            {
                //���� ���� �ִ� image�� �Ⱥ��̰� ���ְ�
                images[a].GetComponent<Image>().DOFade(0, 0.5f);
                images[a].DOScale(0, 0.5f);

                //�����ʿ��ִ� endImage�� ������ ���ش�
                endImages[1].GetComponent<Image>().DOFade(1, 0.5f);
                endImages[1].DOScale(0.75f, 0.5f);
                continue;
            }

            Vector2 temp = new Vector2(positions[(a - 1) % images.Count], images[a].anchoredPosition.y);

            DOTween.To(() => images[a].anchoredPosition, pos => images[a].anchoredPosition = pos, temp, 0.5f);
            images[a].DOScale(scales[(a - 1) % images.Count], 0.5f);
        }

        //endImage�� �������� �ٽ� �����ش�
        SetEndImagePosition();

        yield return new WaitForSeconds(0.5f);

        //ũ��� ��ġ��� �ٽ� ����Ʈ ����
        List<RectTransform> tempList = new List<RectTransform>()
        {
            images[1], images[2], images[3], images[4], endImages[1]
        };

        List<RectTransform> tempEndList = new List<RectTransform>()
        {
            endImages[0], images[0]
        };

        images = tempList;
        endImages = tempEndList;

        //���̶�Ű�信�� ����
        Sorthierarchy();

        //��� �۾��� ���� �� �ٽ� ������ �� ����
        canMove = true;
    }
}
