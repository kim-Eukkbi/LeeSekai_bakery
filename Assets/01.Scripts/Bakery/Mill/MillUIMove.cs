using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MillUIMove : MonoBehaviour
{
    //아이템들의 리스트
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

    //하이라키 상의 정렬정보를 담아놓은 리스트
    private List<int> sort = new List<int>();

    public bool isMoveEnd = true;

    public void SetMillItems(List<MillItem> items)
    {
        this.items = items;

        //sorting정보도 받아준다
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
                //맨왼쪽 안보이는거
                //이놈이 좀 문젠데 아닌가?
                //예는 그냥 반대편으로 옮겨주기만 하면 됨
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[6], DURATION));
            }
            else
            {
                if (a == items.Count - 1)
                {
                    //맨오른쪽 안보이는거
                    //이건 한칸 밀고 스캐일 0.75로 바꾸고 투명에서 원래대로 해야됨
                    moveSeq.Join(items[a].breadImage.DOColor(visibleColor, DURATION));
                }
                else if (a == 1)
                {
                    //보이는거중에 왼쪽
                    //예는 다음위치로 옮겨도 그대로니까 옮겨주고 스케일 0으로 바꾸고 투명하게 해줘야됨
                    moveSeq.Join(items[a].breadImage.DOColor(invisibleColor, DURATION));
                }

                //평범한 경우는 아무것도 안해도 됨
                //위에 두 경우들도 색만 바꿔주면 됨

                //그냥 한칸씩 밀고 크기 바꿔주면 됨
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[a - 1], DURATION));
                moveSeq.Join(items[a].rect.DOScale(scale[a - 1], DURATION));
            }
        }

        moveSeq.AppendCallback(() => isMoveEnd = true);

        //이제 리스트 덮어써주고 시블링인가? 뭐시기 해주면 됨
        //한칸씩 밀렸으니까 이렇게 해주자
        List<MillItem> temp = new List<MillItem>()
        {
            items[1], items[2], items[3], items[4], items[5], items[6], items[0],
        };

        //덮어써주자
        items = temp;

        //매니저에있는거도 덮어써
        MillManager.Instance.items = this.items;

        //정렬도 해주자
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
                //맨오른쪽 안보이는거
                //이놈이 좀 문젠데 아닌가?
                //예는 그냥 반대편으로 옮겨주기만 하면 됨
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[0], DURATION));
            }
            else
            {
                if (a == 0)
                {
                    //맨왼쪽 안보이는거
                    //이건 한칸 밀고 스캐일 0.75로 바꾸고 투명에서 원래대로 해야됨
                    moveSeq.Join(items[a].breadImage.DOColor(visibleColor, DURATION));
                }
                else if (a == items.Count - 2)
                {
                    //보이는거중에 오른쪽
                    //예는 다음위치로 옮겨도 그대로니까 옮겨주고 스케일 0으로 바꾸고 투명하게 해줘야됨
                    moveSeq.Join(items[a].breadImage.DOColor(invisibleColor, DURATION));
                }

                //평범한 경우는 아무것도 안해도 됨
                //위에 두 경우들도 색만 바꿔주면 됨

                //그냥 한칸씩 밀고 크기 바꿔주면 됨
                moveSeq.Join(items[a].rect.DOLocalMoveX(xpos[a + 1], DURATION));
                moveSeq.Join(items[a].rect.DOScale(scale[a + 1], DURATION));
            }
        }

        moveSeq.AppendCallback(() => isMoveEnd = true);

        //이제 리스트 덮어써주고 시블링인가? 뭐시기 해주면 됨
        //한칸씩 밀렸으니까 이렇게 해주자
        List<MillItem> temp = new List<MillItem>()
        {
            items[6], items[0], items[1], items[2], items[3], items[4], items[5],
        };

        //덮어써주자
        items = temp;

        //매니저에있는거도 덮어써
        MillManager.Instance.items = this.items;

        //정렬도 해주자
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