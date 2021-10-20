using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MillUIMove : MonoBehaviour
{
    //�̹������� ����Ʈ
    public List<MillItem> items = new List<MillItem>();
    //���ʿ� ������ �̹���
    public MillItem leftInvisibleItem;
    //�����ʿ� ������ �̹���
    public MillItem rightInvisibleItem;

    private int[] xpos = new int[]
    {
        -670, -360, 0, 360, 670
    };

    private float[] scale = new float[]
    {
        0.75f, 0.9f, 1f, 0.9f, 0.75f
    };

    private Color visibleColor = new Color(1, 1, 1, 1);
    private Color invisibleColor = new Color(1, 1, 1, 0);

    public List<int> sort = new List<int>();

    public void MoveLeft()
    {
        Sequence moveSeq = DOTween.Sequence();

        //���� �����ִ� �̹����� �����ϰ� ���ش�
        moveSeq.AppendCallback(() =>
        {
            items[0].breadImage.color = invisibleColor;
        });

        for (int i = 1; i < 5; i++)
        {
            int a = i;

            Vector2 nextPos = new Vector2(xpos[a - 1], 0);
            Vector2 nextScale = new Vector2(scale[a - 1], scale[a - 1]);

            moveSeq.Join(DOTween.To(() => items[a].rect.anchoredPosition, curPos => items[a].rect.anchoredPosition = curPos, nextPos, 0.5f));
            moveSeq.Join(items[a].rect.DOScale(nextScale, 0.5f));
        }

        moveSeq.AppendCallback(() =>
        {
            //�� �����̹����� �� ���������� �Ű��ش�
            items[0].rect.anchoredPosition = new Vector2(670, 0);
            //�ű� �̹����� �ٽ� ���̰� ���ش�
            items[0].breadImage.color = visibleColor;

            //�ٲ� ���� �°� ����Ʈ�� ������ش�
            List<MillItem> temp = new List<MillItem>()
            {
                items[1], items[2], items[3], items[4], items[0]
            };

            items = temp;
        });

        #region �̹����� Ƣ����� ���� �ִϸ��̼�
        Sequence animSeq = DOTween.Sequence();

        animSeq.AppendCallback(() =>
        {
            leftInvisibleItem.rect.localScale = new Vector2(1, 1);
            leftInvisibleItem.breadImage.color = visibleColor;
        });

        //������ �� �̹��� Ƣ����� ���ְ�
        animSeq.Join(rightInvisibleItem.rect.DOScale(0.75f, 0.5f));
        animSeq.Join(rightInvisibleItem.breadImage.DOColor(visibleColor, 0.5f));

        //���� �� �̹����� ���� ���ش�
        animSeq.Join(leftInvisibleItem.rect.DOScale(0f, 0.5f));
        animSeq.Join(leftInvisibleItem.breadImage.DOColor(invisibleColor, 0.5f));

        animSeq.AppendCallback(() =>
        {
            rightInvisibleItem.rect.localScale = new Vector2(0, 0);
            rightInvisibleItem.breadImage.color = invisibleColor;
        });
        #endregion
    }
    public void MoveRight()
    {
        Sequence moveSeq = DOTween.Sequence();

        //������ �����ִ� �̹����� �����ϰ� ���ش�
        moveSeq.AppendCallback(() =>
        {
            items[4].breadImage.color = invisibleColor;
        });

        for (int i = 0; i < 4; i++)
        {
            int a = i;

            Vector2 nextPos = new Vector2(xpos[a + 1], 0);
            Vector2 nextScale = new Vector2(scale[a + 1], scale[a + 1]);

            moveSeq.Join(DOTween.To(() => items[a].rect.anchoredPosition, curPos => items[a].rect.anchoredPosition = curPos, nextPos, 0.5f));
            moveSeq.Join(items[a].rect.DOScale(nextScale, 0.5f));
        }

        moveSeq.AppendCallback(() =>
        {
            //�� �������̹����� �� �������� �Ű��ش�
            items[4].rect.anchoredPosition = new Vector2(-670, 0);
            //�ű� �̹����� �ٽ� ���̰� ���ش�
            items[4].breadImage.color = visibleColor;

            //�ٲ� ���� �°� ����Ʈ�� ������ش�
            List<MillItem> temp = new List<MillItem>()
            {
                items[4], items[0], items[1], items[2], items[3]
            };

            items = temp;
        });

        #region �̹����� Ƣ����� ���� �ִϸ��̼�
        Sequence animSeq = DOTween.Sequence();

        animSeq.AppendCallback(() =>
        {
            rightInvisibleItem.rect.localScale = new Vector2(1, 1);
            rightInvisibleItem.breadImage.color = visibleColor;
        });

        //���� �� �̹��� Ƣ����� ���ְ�
        animSeq.Join(leftInvisibleItem.rect.DOScale(0.75f, 0.5f));
        animSeq.Join(leftInvisibleItem.breadImage.DOColor(visibleColor, 0.5f));

        //������ �� �̹����� ���� ���ش�
        animSeq.Join(rightInvisibleItem.rect.DOScale(0f, 0.5f));
        animSeq.Join(rightInvisibleItem.breadImage.DOColor(invisibleColor, 0.5f));

        animSeq.AppendCallback(() =>
        {
            leftInvisibleItem.rect.localScale = new Vector2(0, 0);
            leftInvisibleItem.breadImage.color = invisibleColor;
        });
        #endregion
    }

    //�ε��� ���� ���� ������ ��
}