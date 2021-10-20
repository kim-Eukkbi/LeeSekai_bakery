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
    public Button attackBtn;
    public List<Tween> StateTweens;
    private StateUI currentCharacter;
    private GameObject currentPlayer;
    public void Start()
    {
        StateTweens = new List<Tween>();
        attackBtn.onClick.AddListener(() =>
        {
            StartCoroutine(AttackCo());
        });
        StartCoroutine(UpUI());
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

    public void StartFightProcess()
    {
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(characterStateObjs[a].stateSliders[2]
            .DOValue(1, characterStateObjs[a].attackTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Pause();
                SetFight(a);
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

    public void SetFight(int i)
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
        StartCoroutine(SetUIs(i));
        StartCoroutine(MonsterFightSet());
    }

    public IEnumerator MonsterFightSet()
    {
        yield return null;
        monsterObj.transform.DOMoveX(monsterObj.transform.position.x + .5f, .5f);
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
        Uiseq.Append(buttons[0].transform.DOMoveY(buttons[0].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.1f, buttons[1].transform.DOMoveY(buttons[1].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.2f, buttons[2].transform.DOMoveY(buttons[2].transform.position.y + 3.1f, .5f));
        Uiseq.Insert(.3f, buttons[3].transform.DOMoveY(buttons[3].transform.position.y + 3.1f, .5f));
        /*        for(int j =1; j<4;j++)
                {
                    int a = j;
                    Uiseq.Insert(0.1f + (a / 10)*2, buttons[a].transform.DOMoveY(buttons[a].transform.position.y + 3.1f, .5f));
                }*/
        // fightPanel.transform.DOMove(characterStatePanel.transform.position, .8f);
    }

    public IEnumerator AttackCo()
    {
        Sequence Uiseq = DOTween.Sequence();
        Uiseq.Append(buttons[3].transform.DOMoveY(buttons[3].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.1f, buttons[2].transform.DOMoveY(buttons[2].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.2f, buttons[1].transform.DOMoveY(buttons[1].transform.position.y - 3.1f, .5f));
        Uiseq.Insert(.3f, buttons[0].transform.DOMoveY(buttons[0].transform.position.y - 3.1f, .5f));
        //fightPanel.transform.DOMoveY(characterStatePanel.transform.position.y - 5f, .8f);
        currentCharacter.transform.DOMoveY(currentCharacter.transform.position.y - 5, .8f);
        for (int i = 0; i < 3; i++)
        {
            ponCharacterStateObjs[i].SetActive(true);
        }
        yield return new WaitForSeconds(.8f);
        PlayerAttack();
        for (int i = 0; i < 3; i++)
        {
            characterStateObjs[i].transform.position =
                new Vector3(ponCharacterStateObjs[i].transform.position.x,
                characterStateObjs[i].transform.position.y, characterStateObjs[i].transform.position.z);
        }

    }

    public void PlayerAttack()
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 originPos = currentPlayer.transform.position;
        sequence.Append(currentPlayer.transform.DOMove(monsterObj.transform.position, .3f)).SetEase(Ease.OutCirc);
        sequence.Append(currentPlayer.transform.DOMove(originPos, .5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            StartCoroutine(SetDefaultUI());
        }));
        sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f, 5f));  //나중에 다시 생각해보기
    }

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


}
