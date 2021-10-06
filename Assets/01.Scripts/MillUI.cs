using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MillUI : MonoBehaviour
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

    public int[] sortIndexs;

    //보여지는 이미지들의 위치
    public List<RectTransform> images = new List<RectTransform>();
    public List<RectTransform> endImages = new List<RectTransform>(); // 0 = Left, 1 = Right

    private void Start()
    {
        sortIndexs = new int[]
        {
            images[0].GetSiblingIndex(),
            images[1].GetSiblingIndex(),
            images[2].GetSiblingIndex(),
            images[3].GetSiblingIndex(),
            images[4].GetSiblingIndex()
        };

        StartCoroutine(MoveRight());
    }

    /// <summary>
    /// 제작소UI를 오른쪽으로 이동시켜주는 함수
    /// </summary>
    public IEnumerator MoveRight()
    {
        //모든 endImage를 투명하게
        for (int i = 0; i < endImages.Count; i++)
        {
            Image image = endImages[i].GetComponent<Image>();
            Color color = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.color = color;

            endImages[i].localScale = Vector3.zero;
        }

        for (int i = 0; i < images.Count; i++)
        {
            int a = i; //이샛기 클로저라 이렇게 써야됨

            if (i == images.Count - 1) //맨 마지막이라면
            {
                //오른쪽 끝에 있는 image를 안보이게 해주고
                images[a].GetComponent<Image>().DOFade(0, 0.5f);
                images[a].DOScale(0, 0.5f);

                //왼쪽에있는 endImage를 나오게 해준다
                endImages[0].GetComponent<Image>().DOFade(1, 0.5f);
                endImages[0].DOScale(0.75f, 0.5f);
                continue;
            }

            Vector2 temp = new Vector2(positions[(a + 1) % images.Count], images[a].anchoredPosition.y);

            DOTween.To(() => images[a].anchoredPosition, pos => images[a].anchoredPosition = pos, temp, 0.5f);
            images[a].DOScale(scales[(a + 1) % images.Count], 0.5f);
        }

        yield return new WaitForSeconds(0.5f);

        //크기와 위치대로 다시 리스트 세팅
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

        //하이라키뷰에서 정렬
        for (int i = 0; i < 5; i++)
        {
            images[i].SetSiblingIndex(sortIndexs[i]);
        }


    }

    /// <summary>
    /// 제작소UI를 왼쪽으로 이동시켜주는 함수
    /// </summary>
    public void MoveLeft()
    {
        
    }
}
