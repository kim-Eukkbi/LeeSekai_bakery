using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MillUIMove : MonoBehaviour
{
    //�����۵��� ����Ʈ
    private List<MillItem> items = new List<MillItem>();

    private int[] xpos = new int[]
    {
        -670, -670, -360, 0, 360, 670, 670
    };
    private float[] scale = new float[]
    {
        0, 0.75f, 0.9f, 1f, 0.9f, 0.75f, 0
    };

    private Color visibleColor = new Color(1, 1, 1, 1);
    private Color invisibleColor = new Color(1, 1, 1, 0);

    private const float DURATION = 0.5f;

    //���̶�Ű ���� ���������� ��Ƴ��� ����Ʈ
    private List<int> sort = new List<int>();

    public bool isMoveEnd = true;

    public void SetMillItems(List<MillItem> items)
    {
        this.items = items;

        //sorting������ �޾��ش�
        for (int i = 0; i < items.Count; i++)
        {
            sort.Add(items[i].transform.GetSiblingIndex());
        }
    }

    public void MoveLeft()
    {
        if (!isMoveEnd) return;

        isMoveEnd = false;

        Sequence moveSeq = DOTween.Sequence();

        for (int i = 0; i < items.Count; i++)
        {
            int a = i;

            if (a == 0)
            {
                //�ǿ��� �Ⱥ��̴°�
                //�̳��� �� ������ �ƴѰ�?
                //���� �׳� �ݴ������� �Ű��ֱ⸸ �ϸ� ��
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[6], DURATION));
            }
            else
            {
                if (a == items.Count - 1)
                {
                    //�ǿ����� �Ⱥ��̴°�
                    //�̰� ��ĭ �а� ��ĳ�� 0.75�� �ٲٰ� ������ ������� �ؾߵ�
                    moveSeq.Join(items[a].breadImage.DOColor(visibleColor, DURATION));
                }
                else if (a == 1)
                {
                    //���̴°��߿� ����
                    //���� ������ġ�� �Űܵ� �״�δϱ� �Ű��ְ� ������ 0���� �ٲٰ� �����ϰ� ����ߵ�
                    moveSeq.Join(items[a].breadImage.DOColor(invisibleColor, DURATION));
                }

                //����� ���� �ƹ��͵� ���ص� ��
                //���� �� ���鵵 ���� �ٲ��ָ� ��

                //�׳� ��ĭ�� �а� ũ�� �ٲ��ָ� ��
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[a - 1], DURATION));
                moveSeq.Join(items[a].rect.DOScale(scale[a - 1], DURATION));
            }
        }

        moveSeq.AppendCallback(() => isMoveEnd = true);

        //���� ����Ʈ ������ְ� �ú��ΰ�? ���ñ� ���ָ� ��
        //��ĭ�� �з����ϱ� �̷��� ������
        List<MillItem> temp = new List<MillItem>()
        {
            items[1], items[2], items[3], items[4], items[5], items[6], items[0],
        };

        //���������
        items = temp;

        //�Ŵ������ִ°ŵ� �����
        MillManager.Instance.items = this.items;

        //���ĵ� ������
        SortingSiblingIndex();
    }

    public void MoveRight()
    {
        if (!isMoveEnd) return;

        isMoveEnd = false;

        Sequence moveSeq = DOTween.Sequence();

        for (int i = 0; i < items.Count; i++)
        {
            int a = i;

            if (a == items.Count - 1)
            {
                //�ǿ����� �Ⱥ��̴°�
                //�̳��� �� ������ �ƴѰ�?
                //���� �׳� �ݴ������� �Ű��ֱ⸸ �ϸ� ��
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[0], DURATION));
            }
            else
            {
                if (a == 0)
                {
                    //�ǿ��� �Ⱥ��̴°�
                    //�̰� ��ĭ �а� ��ĳ�� 0.75�� �ٲٰ� ������ ������� �ؾߵ�
                    moveSeq.Join(items[a].breadImage.DOColor(visibleColor, DURATION));
                }
                else if (a == items.Count - 2)
                {
                    //���̴°��߿� ������
                    //���� ������ġ�� �Űܵ� �״�δϱ� �Ű��ְ� ������ 0���� �ٲٰ� �����ϰ� ����ߵ�
                    moveSeq.Join(items[a].breadImage.DOColor(invisibleColor, DURATION));
                }

                //����� ���� �ƹ��͵� ���ص� ��
                //���� �� ���鵵 ���� �ٲ��ָ� ��

                //�׳� ��ĭ�� �а� ũ�� �ٲ��ָ� ��
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[a + 1], DURATION));
                moveSeq.Join(items[a].rect.DOScale(scale[a + 1], DURATION));
            }
        }

        moveSeq.AppendCallback(() => isMoveEnd = true);

        //���� ����Ʈ ������ְ� �ú��ΰ�? ���ñ� ���ָ� ��
        //��ĭ�� �з����ϱ� �̷��� ������
        List<MillItem> temp = new List<MillItem>()
        {
            items[6], items[0], items[1], items[2], items[3], items[4], items[5],
        };

        //���������
        items = temp;

        //�Ŵ������ִ°ŵ� �����
        MillManager.Instance.items = this.items;

        //���ĵ� ������
        SortingSiblingIndex();
    }

    private void SortingSiblingIndex()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.SetSiblingIndex(sort[i]);
        }
    }
}