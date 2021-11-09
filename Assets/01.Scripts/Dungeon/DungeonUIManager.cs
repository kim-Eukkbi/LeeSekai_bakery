using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("캐릭터")]
    public GameObject fightPanel;
    public GameObject skillPanel;
    public GameObject characterStatePanel;
    public List<GameObject> PlayerObjs;
    public List<GameObject> ponCharacterStateObjs;
    public List<StateUI> characterStateObjs;
    public List<GameObject> buttons;

    [Header("몬스터")]
    public GameObject monsterObj;
    public GameObject monsterStateUIobj;


    [Header("ECT")]
    public Canvas mainCanvas;
    public List<Tween> StateTweens;
    [HideInInspector]
    public StateUI currentCharacter;
    [HideInInspector]
    public GameObject currentPlayer;
    private float playerMaxHp;
    private float monsterMaxHp;
    public void Start()
    {
        StateTweens = new List<Tween>();
        StartCoroutine(UpUI());
        monsterMaxHp = monsterStateUIobj.GetComponent<MonsterStateUI>().hp;
    }

    public void AttackEachOther(bool isPlayer)
    {
        if(isPlayer)
        {
            MonsterStateUI monsterStateUI = monsterStateUIobj.GetComponent<MonsterStateUI>();
            monsterStateUI.stateSliders[0].DOValue((monsterStateUI.hp - currentCharacter.attackDamage) / monsterMaxHp, .8f);
            monsterStateUI.hp -= currentCharacter.attackDamage;
        }
        else
        {
            currentCharacter.stateSliders[0].DOValue((currentCharacter.hp - monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage) / playerMaxHp, .8f);
            currentCharacter.hp -= monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage;
        }
    }

    public void DownUI()
    {
        Sequence Uiseq = DOTween.Sequence();
        foreach (var item in buttons) item.GetComponent<Button>().interactable = false;
        Uiseq.Append(buttons[3].transform.DOMoveY(buttons[3].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.1f, buttons[2].transform.DOMoveY(buttons[2].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.2f, buttons[1].transform.DOMoveY(buttons[1].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.3f, buttons[0].transform.DOMoveY(buttons[0].transform.position.y - 3.1f, .5f));
    }

    public IEnumerator UpUI()
    {
        yield return null;
        Sequence Main = DOTween.Sequence();
        Main.Append(characterStateObjs[0].transform.DOMove(ponCharacterStateObjs[0].transform.position, .5f));
        Main.Insert(.1f, characterStateObjs[1].transform.DOMove(ponCharacterStateObjs[1].transform.position, .5f));
        Main.Insert(.2f, characterStateObjs[2].transform.DOMove(ponCharacterStateObjs[2].transform.position, .5f));
        Main.Join(monsterStateUIobj.transform.DOMoveX(monsterStateUIobj.transform.position.x - 6.5f, .8f));
        yield return new WaitForSeconds(.8f);
        StartFightProcess();
    }

    #region UI올리기
    public void StartFightProcess()
    {
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(characterStateObjs[a].stateSliders[2]
            .DOValue(1, characterStateObjs[a].attackTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Pause();
                SetFightUI(a);
            }));
            StateTweens[i].SetAutoKill(false);
        }

        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(characterStateObjs[a].stateSliders[2].DOValue(0, .5f).SetEase(Ease.Linear));
            StateTweens[3 + a].Pause().SetAutoKill(false);
        }
    }
    public void SetFightUI(int i)
    {
        for (int j = 0; j < characterStateObjs.Count; j++)
        {
            if (i == j) continue;
            StateTweens[j].Pause();
            characterStateObjs[j].transform.DOMoveY(characterStateObjs[j].transform.position.y - 5, .8f);
            PlayerObjs[j].transform.DOMoveX(PlayerObjs[j].transform.position.x - .5f, .5f);
            ponCharacterStateObjs[j].gameObject.SetActive(false);
        }
        currentCharacter = characterStateObjs[i];
        currentPlayer = PlayerObjs[i];
        playerMaxHp = currentCharacter.hp;
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x + .5f, .5f);
        StartCoroutine(SetUIs(i)); 
    }
    public IEnumerator SetUIs(int i)
    {
        yield return new WaitForSeconds(.5f);
        StateTweens[3 + i].Rewind();
        StateTweens[3 + i].Play().OnComplete(() =>
        {
            StateTweens[i].Rewind();
        });

        PlayerObjs[i].transform.DOMoveX(PlayerObjs[i].transform.position.x + .5f, .5f);
        characterStateObjs[i].transform.DOMove(ponCharacterStateObjs[i].transform.position, .8f);
        yield return new WaitForSeconds(.8f);
        Sequence Uiseq = DOTween.Sequence();
        foreach(var item in buttons) item.GetComponent<Button>().interactable = false;
        Uiseq.Append(buttons[0].transform.DOMoveY(buttons[0].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.1f, buttons[1].transform.DOMoveY(buttons[1].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.2f, buttons[2].transform.DOMoveY(buttons[2].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.3f, buttons[3].transform.DOMoveY(buttons[3].transform.position.y + 3.1f, .5f)).OnComplete(() =>
        {foreach (var item in buttons) item.GetComponent<Button>().interactable = true;});
    }
    #endregion

    public IEnumerator SetDefaultUI()
    {
        //몬스터 UI 옆으로 빼기
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x - .5f, .5f);
       // monsterStateUIobj.transform.DOMoveX(monsterStateUIobj.transform.position.x + 6.5f, .5f);
        //StateUI 원위치로
        //플레이어 오브젝트 원위치로
        for (int i = 0; i < 3; i++)
        {
            int a = i;
            PlayerObjs[a].transform.DOMoveX(currentPlayer.transform.position.x - .5f, .5f);
            characterStateObjs[a].transform.DOMoveY(ponCharacterStateObjs[a].transform.position.y, .5f);
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 3; i++) StateTweens[i].Play();
        monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Play();
    }

    public bool MeeleAttackCheck()
    {
        return currentCharacter.isMeeleAttack;
    }


}
