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

    //��ĳ�̽��޴����� selectPanel���� ���õ� ���� �����ϰ� ShowcaseItem�� �־��ش�
}
