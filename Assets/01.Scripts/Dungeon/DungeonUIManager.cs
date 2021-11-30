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
    public List<GameObject> fightbuttons;

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
        StartCoroutine(UpStateUI());
        StartCoroutine(StartFightProcess());
        monsterMaxHp = monsterStateUIobj.GetComponent<MonsterStateUI>().hp;
    }

    public void AttackEachOther(bool isPlayer) //서로 때리는걸 계산하는 부분
    {
        if(isPlayer)
        {
            MonsterStateUI monsterStateUI = monsterStateUIobj.GetComponent<MonsterStateUI>();
            monsterStateUI.stateSliders[0].DOValue((monsterStateUI.hp - currentCharacter.attackDamage) / monsterMaxHp, .8f);
            if(monsterStateUI.hp <= 0)
            {
                monsterStateUI.DeadMonster();
            }
            else
            {
                monsterStateUI.hp -= currentCharacter.attackDamage;
            } 
        }
        else
        {
            currentCharacter.stateSliders[0].DOValue((currentCharacter.hp - monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage) / playerMaxHp, .8f);
            currentCharacter.hp -= monsterStateUIobj.GetComponent<MonsterStateUI>().attackDamage;
        }
    }

    public void DownFightUI() //준비되었을때 매뉴를 내리는 함수
    {
        Sequence Uiseq = DOTween.Sequence();
        foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = false;
        Uiseq.Append(fightbuttons[3].transform.DOMoveY(fightbuttons[3].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.1f, fightbuttons[2].transform.DOMoveY(fightbuttons[2].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.2f, fightbuttons[1].transform.DOMoveY(fightbuttons[1].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.3f, fightbuttons[0].transform.DOMoveY(fightbuttons[0].transform.position.y - 3.1f, .5f));
    }

    public IEnumerator UpStateUI() // 맨 처음 StateUI를 샤라락 올리는 함수
    {
        yield return null;
        Sequence Main = DOTween.Sequence();
        Main.Append(characterStateObjs[0].transform.DOMove(ponCharacterStateObjs[0].transform.position, .5f));
        Main.Insert(.1f, characterStateObjs[1].transform.DOMove(ponCharacterStateObjs[1].transform.position, .5f));
        Main.Insert(.2f, characterStateObjs[2].transform.DOMove(ponCharacterStateObjs[2].transform.position, .5f));
        Main.Join(monsterStateUIobj.transform.DOMoveX(monsterStateUIobj.transform.position.x - 6.5f, .8f));
    }

    #region 전투 시작
    public IEnumerator StartFightProcess() // UpStateUI 가 끝날때 까지 기다린 이후에 전투를 준비하는 함수
    {
        yield return new WaitForSeconds(.8f);
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(characterStateObjs[a].stateSliders[2]
            .DOValue(1, characterStateObjs[a].attackTime).SetEase(Ease.Linear).OnComplete(() => //플레이어의 전투게이지를 채우는 코드
            {
                monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Pause(); // 플레이어의 전투게이지가 꽉 찼다면 몬스터의 전투 게이지를 멈춤
                SetFightUI(a); //전투매뉴를 올림 a는 3명중 하나를 알기 위한 Index
            }));
            StateTweens[i].SetAutoKill(false); //닷트윈을 Rewind 시키기 위한 오토킬 해제
        }

        for (int i = 0; i < characterStateObjs.Count; i++) //전투 게이지가 꽉 찼을때 자연스럽게 줄여주는 닷트윈을 추가
        {
            int a = i; //닷트윈은 i 바로 쓰면 안됨
            StateTweens.Add(characterStateObjs[a].stateSliders[2].DOValue(0, .5f).SetEase(Ease.Linear));  //줄여주는 닷트윈을 원래 있던 채우는 트윈 뒷쪽에 추가
            StateTweens[3 + a].Pause().SetAutoKill(false);  //바로 줄어들면 안되니까 닷트윈 멈추고 리와인드 
        }
    }
    public void SetFightUI(int i) //
    {
        for (int j = 0; j < characterStateObjs.Count; j++)
        {
            if (i == j) continue;
            StateTweens[j].Pause();
            characterStateObjs[j].transform.DOMoveY(characterStateObjs[j].transform.position.y - 5, .8f);
            //PlayerObjs[j].transform.DOMoveX(PlayerObjs[j].transform.position.x - .5f, .5f);
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

        //PlayerObjs[i].transform.DOMoveX(PlayerObjs[i].transform.position.x + .5f, .5f);
        characterStateObjs[i].transform.DOMove(ponCharacterStateObjs[i].transform.position, .8f);
        yield return new WaitForSeconds(.8f);
        Sequence Uiseq = DOTween.Sequence();
        foreach(var item in fightbuttons) item.GetComponent<Button>().interactable = false;
        Uiseq.Append(fightbuttons[0].transform.DOMoveY(fightbuttons[0].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.1f, fightbuttons[1].transform.DOMoveY(fightbuttons[1].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.2f, fightbuttons[2].transform.DOMoveY(fightbuttons[2].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.3f, fightbuttons[3].transform.DOMoveY(fightbuttons[3].transform.position.y + 3.1f, .5f)).OnComplete(() =>
        {foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = true;});
    }
    #endregion

    public IEnumerator SetDefaultUI()
    {
        //몬스터 원위치
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x - .5f, .5f);
        
        for (int i = 0; i < 3; i++)
        {
            int a = i;
            //플레이어 오브젝트 원위치로
            // PlayerObjs[a].transform.DOMoveX(currentPlayer.transform.position.x - .5f, .5f);
            //StateUI 원위치로
            characterStateObjs[a].transform.DOMoveY(ponCharacterStateObjs[a].transform.position.y, .5f);
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 3; i++) StateTweens[i].Play();
        monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Play();
    }


}
