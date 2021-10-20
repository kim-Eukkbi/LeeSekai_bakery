using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MillUIMove : MonoBehaviour
{
    //이미지들의 리스트
    public List<MillItem> items = new List<MillItem>();
    //왼쪽에 숨겨진 이미지
    public MillItem leftInvisibleItem;
    //오른쪽에 숨겨진 이미지
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

        //왼쪽 끝에있던 이미지를 투명하게 해준다
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
            //맨 왼쪽이미지를 맨 오른쪽으로 옮겨준다
            items[0].rect.anchoredPosition = new Vector2(670, 0);
            //옮긴 이미지를 다시 보이게 해준다
            items[0].breadImage.color = visibleColor;

            //바뀐 값에 맞게 리스트를 덮어써준다
            List<MillItem> temp = new List<MillItem>()
            {
                items[1], items[2], items[3], items[4], items[0]
            };

            items = temp;
        });

        #region 이미지가 튀어나오고 들어가는 애니메이션
        Sequence animSeq = DOTween.Sequence();

        animSeq.AppendCallback(() =>
        {
            leftInvisibleItem.rect.localScale = new Vector2(1, 1);
            leftInvisibleItem.breadImage.color = visibleColor;
        });

        //오른쪽 끝 이미지 튀어나오게 해주고
        animSeq.Join(rightInvisibleItem.rect.DOScale(0.75f, 0.5f));
        animSeq.Join(rightInvisibleItem.breadImage.DOColor(visibleColor, 0.5f));

        //왼쪽 끝 이미지가 들어가게 해준다
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

        //오른쪽 끝에있던 이미지를 투명하게 해준다
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
            //맨 오른쪽이미지를 맨 왼쪽으로 옮겨준다
            items[4].rect.anchoredPosition = new Vector2(-670, 0);
            //옮긴 이미지를 다시 보이게 해준다
            items[4].breadImage.color = visibleColor;

            //바뀐 값에 맞게 리스트를 덮어써준다
            List<MillItem> temp = new List<MillItem>()
            {
                items[4], items[0], items[1], items[2], items[3]
            };

            items = temp;
        });

        #region 이미지가 튀어나오고 들어가는 애니메이션
        Sequence animSeq = DOTween.Sequence();

        animSeq.AppendCallback(() =>
        {
            rightInvisibleItem.rect.localScale = new Vector2(1, 1);
            rightInvisibleItem.breadImage.color = visibleColor;
        });

        //왼쪽 끝 이미지 튀어나오게 해주고
        animSeq.Join(leftInvisibleItem.rect.DOScale(0.75f, 0.5f));
        animSeq.Join(leftInvisibleItem.breadImage.DOColor(visibleColor, 0.5f));

        //오른쪽 끝 이미지가 들어가게 해준다
        animSeq.Join(rightInvisibleItem.rect.DOScale(0f, 0.5f));
        animSeq.Join(rightInvisibleItem.breadImage.DOColor(invisibleColor, 0.5f));

        animSeq.AppendCallback(() =>
        {
            leftInvisibleItem.rect.localScale = new Vector2(0, 0);
            leftInvisibleItem.breadImage.color = invisibleColor;
        });
        #endregion
    }

    //인덱스 정렬 아직 안했음 ㅋ
}