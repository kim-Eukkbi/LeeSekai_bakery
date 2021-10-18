using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DungeonUIManager : MonoBehaviour
{
    public static DungeonUIManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("다수의 UImanager가 실행중입니다 하나만 남기고 삭제합니다");
            Destroy(instance);
        }
        instance = this;
    }

    public List<GameObject> ponCharacterStateObjs;
    public List<StateUI> characterStateObjs;
    private List<Tween> StateTweens;
    public void Start()
    {
        StateTweens = new List<Tween>();
        SetDefaultUI();
    }

    public void SetDefaultUI()
    {
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(DOTween.To(() => characterStateObjs[a].stateSliders[2].value,
            x => characterStateObjs[a].stateSliders[2].value = x, 1,
            characterStateObjs[a].attackTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                SetFight(a);
            }));
        }
    }

    public void SetFight(int i)
    {
        for (int j = 0; j < characterStateObjs.Count; j++)
        {
            if (i == j) continue;
            StateTweens[j].Kill();
            characterStateObjs[j].transform.DOMoveY(characterStateObjs[j].transform.position.y - 5, .8f);
            ponCharacterStateObjs[j].gameObject.SetActive(false);
        }
        StartCoroutine(SetCharacterObj(i));
    }

    public IEnumerator SetCharacterObj(int i)
    {
        yield return new WaitForSeconds(.8f);
        characterStateObjs[i].transform.DOMove(ponCharacterStateObjs[i].transform.position, .8f).SetEase(Ease.OutQuad);
    }


}
