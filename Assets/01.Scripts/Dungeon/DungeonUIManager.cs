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
            Debug.Log("�ټ��� UImanager�� �������Դϴ� �ϳ��� ����� �����մϴ�");
            Destroy(instance);
        }
        instance = this;
    }

    [Header("ĳ����")]
    public GameObject fightPanel;
    public GameObject characterStatePanel;
    public List<GameObject> PlayerObjs;
    public List<GameObject> ponCharacterStateObjs;
    public List<StateUI> characterStateObjs;

    [Header("����")]
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
        StartFightProcess();
    }

    public void StartFightProcess()
    {
        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(DOTween.To(() => characterStateObjs[a].stateSliders[2].value,
            x => characterStateObjs[a].stateSliders[2].value = x, 1,
            characterStateObjs[a].attackTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Pause();
                SetFight(a);
            }));
            StateTweens[i].SetAutoKill(false);
        }

        for (int i = 0; i < characterStateObjs.Count; i++)
        {
            int a = i;
            StateTweens.Add(DOTween.To(() => characterStateObjs[a].stateSliders[2].value,
            x => characterStateObjs[a].stateSliders[2].value = x, 0,.5f).SetEase(Ease.Linear));
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
            PlayerObjs[j].transform.DOMoveY(PlayerObjs[j].transform.position.y - .5f, .5f);
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
        monsterObj.transform.DOMoveY(monsterObj.transform.position.y + .5f, .5f);
        yield return new WaitForSeconds(.5f);
        monsterStateUIobj.transform.DOMoveX(monsterStateUIobj.transform.position.x - 6.5f,.8f);
    }

    public IEnumerator SetUIs(int i)
    {
        yield return new WaitForSeconds(.5f);
        StateTweens[3 + i].Rewind();
        StateTweens[3 + i].Play().OnComplete(()=>
        {
            StateTweens[i].Rewind();
        });
        
        PlayerObjs[i].transform.DOMoveY(PlayerObjs[i].transform.position.y + .5f, .5f);
        characterStateObjs[i].transform.DOMove(ponCharacterStateObjs[i].transform.position, .8f);
        yield return new WaitForSeconds(.8f);
        fightPanel.transform.DOMove(characterStatePanel.transform.position, .8f);
    }

    public IEnumerator AttackCo()
    {
        fightPanel.transform.DOMoveY(characterStatePanel.transform.position.y - 5f, .8f);
        currentCharacter.transform.DOMoveY(currentCharacter.transform.position.y - 5, .8f);
        for(int i =0; i <3;i++)
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
        sequence.Append(currentPlayer.transform.DOMove(monsterObj.transform.position, .3f).SetLoops(2, LoopType.Yoyo)).OnComplete(()=>
        {
           StartCoroutine(SetDefaultUI());
        });
        sequence.Insert(.2f, Camera.main.DOShakeRotation(.1f,5f));  //���߿� �ٽ� �����غ���
    }

    public IEnumerator SetDefaultUI()
    {
        //���� UI ������ ����
        monsterObj.transform.DOMoveY(monsterObj.transform.position.y - .5f, .5f);
        monsterStateUIobj.transform.DOMoveX(monsterStateUIobj.transform.position.x + 6.5f, .5f);
        //StateUI ����ġ��
        //�÷��̾� ������Ʈ ����ġ��
        for (int i =0; i< 3;i++)
        {
            int a = i;
            PlayerObjs[a].transform.DOMoveY(currentPlayer.transform.position.y - .5f, .5f);
            characterStateObjs[a].transform.DOMoveY(ponCharacterStateObjs[a].transform.position.y,.5f);
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 3; i++) StateTweens[i].Play();
        monsterStateUIobj.GetComponent<MonsterStateUI>().readyAttack[0].Play();
    }


}
