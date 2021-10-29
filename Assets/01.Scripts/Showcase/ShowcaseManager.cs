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

    //쇼캐이스메니저는 selectPanel에서 선택된 빵을 관리하고 ShowcaseItem에 넣어준다

    //1. ShowcaseItem이 눌린다 (이때 자신이 무슨 item인지 매개변수로 전달)
    //2. SelectPanel이 열린다 (당연히 showcasePanel은 잠깐 꺼져야 함)
    //3. 선택한 아이템이 눌린다 (이때도 자신이 무슨 빵인지 매개변수로 전달해야함)
    //4. 확인버튼을 누르면 선택한 아이템을 showcaseItem의 빵으로 바꿔준다
}
