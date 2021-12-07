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
    public State playerCurrentState;

    [Header("몬스터")]
    public GameObject monsterObj;
    public GameObject monsterStateUIobj;
    public State monsterCurrentState;


    [Header("ECT")]
    public Canvas mainCanvas;
    public List<Tween> stateTweens;
    [HideInInspector]
    public StateUI currentCharacterStateUI;
    [HideInInspector]
    public GameObject currentPlayer;
    private float playerMaxHp;
    private float monsterMaxHp;
    [HideInInspector]
    public List<State> playerState = new List<State>();// 캐릭터의 현재 상태를 정의
    private int currentPlayerIndex = 0;
    public GameObject gameEndObj;   
    public GameObject gameEndObjDoma;   
    public GameObject gameEndObjDomainsideImg;   
    public GameObject gameEndObjDomainsideIndex;   
    public GameObject gameEndObjVic;   
    public GameObject gameEndObjContinue;   



    public void Start()
    {
        monsterCurrentState = State.Live; // 몬스터의 현재 상태를 정의
        playerCurrentState = State.Live;
        stateTweens = new List<Tween>();
        StartCoroutine(UpStateUI());
        StartCoroutine(StartFightProcess());
        monsterMaxHp = monsterStateUIobj.GetComponent<MonsterStateUI>().hp;
        playerState.Add(State.Live);
        playerState.Add(State.Live);
        playerState.Add(State.Live);
    }

 /*   public void Update()
    {
        if(playerState[currentPlayerIndex] == State.Dead)
        {
            stateTweens[currentPlayerIndex].Pause(); //공격 트윈 중지
        }*/
    //}

    public void AttackEachOther(bool isPlayer) //서로 때리는걸 계산하는 부분
    {
        if (isPlayer)
        {
            MonsterStateUI monsterStateUI = monsterStateUIobj.GetComponent<MonsterStateUI>();
            float result = currentCharacterStateUI.str - monsterStateUI.def < 0 ? 1f : currentCharacterStateUI.str - monsterStateUI.def;
            Debug.Log(result);
            monsterStateUI.stateSliders[0].DOValue((monsterStateUI.hp - result) / monsterMaxHp, .8f); //적의 체력 게이지에서 받은 데미지를 빼는 계산을 한다

            monsterStateUI.hp -= result; // 피깎기


            if (monsterStateUI.hp <= 0) //만약 피가 0 이하라면 
            {
                monsterStateUI.DeadMonster(); //적 죽음 함수를 실행
                monsterCurrentState = State.Dead;
            }
        }
        else
        {
            float result = monsterStateUIobj.GetComponent<MonsterStateUI>().str - currentCharacterStateUI.def <
                0 ? 1f : monsterStateUIobj.GetComponent<MonsterStateUI>().str - currentCharacterStateUI.def;
            Debug.Log(result);
            currentCharacterStateUI.stateSliders[0].DOValue((currentCharacterStateUI.hp - result) / playerMaxHp, .8f); //적이 나를 때렸을때 가장 최근에 공격한 플레이어의 체력 게이지를 계산 하여 깎음
            currentCharacterStateUI.hp -= result; //피 깎기

            if(currentCharacterStateUI.hp <= 0) // 만약 피가 0이하라면
            {
                playerState[currentPlayerIndex] = State.Dead;
                PlayerDead(); // 플레이어 죽음 함수
               // Debug.Log(playerState[currentPlayerIndex]);
            }
        }
    }

    public void PlayerDead()
    {
        currentCharacterStateUI.GetComponentInChildren<CanvasGroup>().DOFade(1, .5f); //켄버스 그룹에서 찾아 어둡게 하는 함수
        stateTweens[currentPlayerIndex].Pause(); //공격 트윈 중지
        stateTweens[3 + currentPlayerIndex].Pause(); //공격 트윈 중지
        PlayerObjs[currentPlayerIndex].SetActive(false); //플레이어 오브젝트 끄기
    }

    public void DownFightUI() //한번의 타격이 끝난 후 전투매뉴를 내리는 함수
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
    }

    #region 전투 시작
    public IEnumerator StartFightProcess() // UpStateUI 가 끝날때 까지 기다린 이후에 전투를 준비하는 함수
    {
        yield return new WaitForSeconds(.8f);
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            stateTweens.Add(characterStateObjs[a].stateSliders[2]
            .DOValue(1, characterStateObjs[a].sp).SetEase(Ease.Linear).OnComplete(() => //플레이어의 전투게이지를 채우는 코드
            {
                monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Pause(); // 플레이어의 전투게이지가 꽉 찼다면 몬스터의 전투 게이지를 멈춤
                SetFightUI(a); //전투매뉴를 올림 a는 3명중 하나를 알기 위한 Index
            }));
            stateTweens[i].SetAutoKill(false); //닷트윈을 Rewind 시키기 위한 오토킬 해제
        }

        for (int i = 0; i < characterStateObjs.Count; i++) //전투 게이지가 꽉 찼을때 자연스럽게 줄여주는 닷트윈을 추가
        {
            int a = i; //닷트윈은 i 바로 쓰면 안됨
            stateTweens.Add(characterStateObjs[a].stateSliders[2].DOValue(0, .5f).SetEase(Ease.Linear));  //줄여주는 닷트윈을 원래 있던 채우는 트윈 뒷쪽에 추가
            stateTweens[3 + a].Pause().SetAutoKill(false);  //바로 줄어들면 안되니까 닷트윈 멈추고 리와인드 
        }
    }
    public void SetFightUI(int i) //3명중 2명 UI를 내리고 전투매뉴를 올림
    {
        for (int j = 0; j < characterStateObjs.Count; j++) //나머지 2명의 UI를 내리고 그들의 트윈도 중지함
        {
            if (i == j) continue;
            stateTweens[j].Pause(); // 나머지 2명 트윈 중지
            characterStateObjs[j].transform.DOMoveY(characterStateObjs[j].transform.position.y - 5, .8f);// 나머지 2명 UI 내리기
            ponCharacterStateObjs[j].gameObject.SetActive(false); //부드러운 움직임을 위한 비어있는 오브젝트 꺼서 정렬 하기
        }
        currentPlayerIndex = i;
        currentCharacterStateUI = characterStateObjs[i]; //선택된 캐릭터의 UI를 저장
        currentPlayer = PlayerObjs[i]; //선택된 캐릭터의 오브젝트를 저장
        playerCurrentState = playerState[i];
        playerMaxHp = currentCharacterStateUI.hp; // 선택된 캐릭터의 최대 채력을 저장


        monsterObj.transform.DOMoveX(monsterObj.transform.position.x + .5f, .5f);// 몬스터의 위치를 조금 뒤로
        StartCoroutine(SetFightMenuUiUP(i));
    }
    public IEnumerator SetFightMenuUiUP(int i) //2명의 UI를 내렸으니 선택된 캐릭터의 UI를 왼쪽으로 옴기고 전투 매뉴를 올림
    {
        yield return new WaitForSeconds(.5f);
        stateTweens[3 + i].Rewind(); //전투 게이지를 줄이는 트윈을 초기화 함
        stateTweens[3 + i].Play().OnComplete(() => //전투게이지를 줄이고 초기화 한 뒤 시작하고 초기화를 완료하면 전투 게이지를 채우는 트윈을 초기화 함
        {
            stateTweens[i].Rewind();//전투게이지 트윈 초기화
        });

        //PlayerObjs[i].transform.DOMoveX(PlayerObjs[i].transform.position.x + .5f, .5f);
        characterStateObjs[i].transform.DOMove(ponCharacterStateObjs[i].transform.position, .8f); //선택된 캐릭터를 정렬되어있는 비어있는 오브젝트의 위치로 옴기는 트윈
        yield return new WaitForSeconds(.8f);
        UPFightUI(); //FightUI를 올리는 함수
    }

    public void UPFightUI() //FightUI를 올리는 함수
    {
        Sequence Uiseq = DOTween.Sequence();
        foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = false; //올라가는 중에 버튼이 눌리면 버그가 날 수 있으니 interactable을 꺼줌
        Uiseq.Append(fightbuttons[0].transform.DOMoveY(fightbuttons[0].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.1f, fightbuttons[1].transform.DOMoveY(fightbuttons[1].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.2f, fightbuttons[2].transform.DOMoveY(fightbuttons[2].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.3f, fightbuttons[3].transform.DOMoveY(fightbuttons[3].transform.position.y + 3.1f, .5f)).OnComplete(() => // 샤라락 올라오는 효과를 닷드윈 시퀸스로 만듬
        { foreach (var item in fightbuttons) item.GetComponent<Button>().interactable = true; }); // 마지막 버튼까지 올라오고 나면 아까 꺼뒀던 interactable을 켜줌
    }
    #endregion

    public IEnumerator SetDefaultUI() //모든 UI를 초기상태로 돌려주는 함수
    {
        //몬스터 원위치
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x - .5f, .5f); // 아까 살짝 민거 다시 땡기기
        StartCoroutine(UpStateUI());//StateUI를 원위치로
        yield return new WaitForSeconds(.8f);
        for (int i = 0; i < 3; i++)
        {
            if (playerState[i].Equals(State.Dead))
            {
                stateTweens[i].Pause();
                Debug.Log("Stop Living");
                continue;
            }
                
            stateTweens[i].Play(); // 다시 전투 게이지를 채워주는 트윈을 시작함
        }
        monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Play(); //적 전투 게이지를 채워주는 트윈을 시작함
    }


}
