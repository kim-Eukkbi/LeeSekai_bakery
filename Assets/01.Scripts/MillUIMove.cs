using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MillUIMove : MonoBehaviour
{
    //이미지들의 x위치값
    public readonly int[] positions =
    {
        -670, -360, 0, 360, 670
    };
    //이미지들의 크기
    public readonly float[] scales =
    {
        0.75f, 0.9f, 1f, 0.9f, 0.75f
    };

    //부모오브젝트의 위치
    public Transform parent;
    //생성할 프리팹
    public MillItem millItemPrefab;

    //프리팹 생성 순서
    public int[] creationOrder = new int[]
    {
        3, 2, 4, 1, 5, 0, 6
    };

    //양 끝 이미지의 인덱스
    public int[] sideItemIdx = new int[]
    {
        0, 6
    };

    //하이라키상에서의 위치
    private int[] sortIndexs;
    private int[] sortEndIndexes;

    //Dotween이 끝났는지
    public bool canMove = true;

    //보여지는 이미지들의 리스트
    public List<MillItem> items = new List<MillItem>();

    public void InitMillUI()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i] = Instantiate(millItemPrefab, parent);
        }
        //초기화 해주는 작업을 아직 안끝냈음
    }

    /// <summary>
    /// 제작소 UI들을 오른쪽으로 이동시켜주는 함수
    /// </summary>
    public void MoveRight()
    {
        if(canMove)
        {
            StartCoroutine(MoveRightRoutine());
        }
    }

    /// <summary>
    /// 제작소 UI들을 왼쪽으로 이동시켜주는 함수
    /// </summary>
    public void MoveLeft()
    {
        if (canMove)
        {
            StartCoroutine(MoveLeftRoutine());
        }
    }

    /// <summary>
    /// 양 끝 이미지를 투명하게 만들어주는 함수
    /// </summary>
    private void SetEndImageInvisible()
    {
        //모든 endImage를 투명하게
        for (int i = 0; i < 2; i++)
        {
            Image image = items[sideItemIdx[i]].GetComponent<Image>();
            Color color = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.color = color;

            items[sideItemIdx[i]].transform.localScale = Vector3.zero;
        }
    }

    /// <summary>
    /// 하이라키뷰에서 정렬해주는 함수
    /// </summary>
    private void Sorthierarchy()
    {
        //하이라키뷰에서 정렬
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.SetSiblingIndex(sortIndexs[i]);
        }
    }

    /// <summary>
    /// endImage의 position을 세팅해주는 함수
    /// </summary>
    private void SetSideImagePosition()
    {
        items[sideItemIdx[0]].rect.anchoredPosition = new Vector2(-670, items[sideItemIdx[0]].rect.anchoredPosition.y);
        items[sideItemIdx[1]].rect.anchoredPosition = new Vector2(670, items[sideItemIdx[0]].rect.anchoredPosition.y);
    }

    /// <summary>
    /// 제작소UI를 오른쪽으로 이동시켜주는 루틴
    /// </summary>
    private IEnumerator MoveRightRoutine()
    {
        //Dotween이 안끝났으니 못움직이게
        canMove = false;

        //모든 endImage를 투명하게
        SetEndImageInvisible();

        for (int i = 0; i < items.Count; i++)
        {
            int a = i; //이샛기 클로저라 이렇게 써야됨

            if (i == items.Count - 1) //맨 마지막이라면
            {
                //오른쪽 끝에 있는 image를 안보이게 해주고
                items[a].GetComponent<Image>().DOFade(0, 0.5f);
                items[a].transform.DOScale(0, 0.5f);

                //왼쪽에있는 endImage를 나오게 해준다
                items[sideItemIdx[0]].GetComponent<Image>().DOFade(1, 0.5f);
                items[sideItemIdx[0]].transform.DOScale(0.75f, 0.5f);
                continue;
            }

            Vector2 temp = new Vector2(positions[(a + 1) % items.Count], items[a].rect.anchoredPosition.y);

            DOTween.To(() => items[a].rect.anchoredPosition, pos => items[a].rect.anchoredPosition = pos, temp, 0.5f);
            items[a].rect.DOScale(scales[(a + 1) % items.Count], 0.5f);
        }

        //endImage의 포지션을 다시 맞춰준다
        SetSideImagePosition();

        yield return new WaitForSeconds(0.5f);

        //크기와 위치대로 다시 리스트 세팅
        List<MillItem> tempList = new List<MillItem>()
        {
            items[4], items[sideItemIdx[0]], items[0], items[1], items[2], items[3], items[sideItemIdx[1]]
        };

        items = tempList;

        //하이라키뷰에서 정렬
        Sorthierarchy();

        //모든 작업이 끝난 후 다시 움직일 수 있음
        canMove = true;
    }

    /// <summary>
    /// 제작소UI를 왼쪽으로 이동시켜주는 루틴
    /// </summary>
    private IEnumerator MoveLeftRoutine()
    {
        //Dotween이 안끝났으니 못움직이게
        canMove = false;

        //모든 endImage를 투명하게
        SetEndImageInvisible();

        for (int i = 0; i < items.Count; i++)
        {
            int a = i; //이샛기 클로저라 이렇게 써야됨

            if (i == 0) //맨 처음이라면
            {
                //왼쪽 끝에 있는 image를 안보이게 해주고
                items[a].GetComponent<Image>().DOFade(0, 0.5f);
                items[a].transform.DOScale(0, 0.5f);

                //오른쪽에있는 endImage를 나오게 해준다
                items[sideItemIdx[1]].GetComponent<Image>().DOFade(1, 0.5f);
                items[sideItemIdx[1]].transform.DOScale(0.75f, 0.5f);
                continue;
            }

            Vector2 temp = new Vector2(positions[(a - 1) % items.Count], items[a].rect.anchoredPosition.y);

            DOTween.To(() => items[a].rect.anchoredPosition, pos => items[a].rect.anchoredPosition = pos, temp, 0.5f);
            items[a].rect.DOScale(scales[(a - 1) % items.Count], 0.5f);
        }

        //endImage의 포지션을 다시 맞춰준다
        SetSideImagePosition();

        yield return new WaitForSeconds(0.5f);

        //크기와 위치대로 다시 리스트 세팅
        List<MillItem> tempList = new List<MillItem>()
        {
            items[sideItemIdx[0]], items[1], items[2], items[3], items[4], items[sideItemIdx[1]], items[0]
        };

        items = tempList;

        //하이라키뷰에서 정렬
        Sorthierarchy();

        //모든 작업이 끝난 후 다시 움직일 수 있음
        canMove = true;
    }
}
