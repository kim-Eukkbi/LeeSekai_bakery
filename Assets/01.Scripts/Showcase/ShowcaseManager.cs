using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseManager : MonoBehaviour
{
    public static ShowcaseManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }
    }

    //쇼캐이스메니저는 selectPanel에서 선택된 빵을 관리하고 ShowcaseItem에 넣어준다
}
